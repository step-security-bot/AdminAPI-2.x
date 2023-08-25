// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Linq;
using NUnit.Framework;
using EdFi.Ods.AdminApi.Infrastructure.ClaimSetEditor;
using Shouldly;
using System.Collections.Generic;
using Application = EdFi.Security.DataAccess.Models.Application;
using ClaimSet = EdFi.Security.DataAccess.Models.ClaimSet;

namespace EdFi.Ods.AdminApi.DBTests.ClaimSetEditorTests;

[TestFixture]
public class OverrideDefaultAuthorizationStrategyCommandTests : SecurityDataTestBase
{
    [Test]
    public void ShouldOverrideAuthorizationStrategiesForParentResourcesOnClaimSet()
    {
        InitializeData(out var testClaimSet, out var appAuthorizationStrategies, out var testResource1ToEdit, out var testResource2ToNotEdit);
        var overrides = new List<AuthorizationStrategy>();
        if (appAuthorizationStrategies != null)
        {
            foreach(var appAuthorizationStrategy in appAuthorizationStrategies)
            {
                overrides.Add(new AuthorizationStrategy
                {
                    AuthStrategyId = appAuthorizationStrategy.AuthorizationStrategyId,
                    AuthStrategyName = appAuthorizationStrategy.AuthorizationStrategyName
                });
            }
        }

        var overrideModel = new OverrideAuthorizationStrategyModel
        {
            ResourceClaimId = testResource1ToEdit.ResourceClaimId,
            ClaimSetId = testClaimSet.ClaimSetId,
            ClaimSetResourceClaimActionAuthStrategyOverrides = new List<ClaimSetResourceClaimActionAuthStrategies> {
                new ClaimSetResourceClaimActionAuthStrategies
                {
                    ActionId = 1,
                    ActionName= "Create",
                    AuthorizationStrategies = overrides
                }
            } 
        };

        List<ResourceClaim> resourceClaimsForClaimSet = null;

        using var securityContext = TestContext;
        var command = new OverrideDefaultAuthorizationStrategyCommand(securityContext);
        command.Execute(overrideModel);
        var getResourcesByClaimSetIdQuery = new GetResourcesByClaimSetIdQuery(securityContext, SecurityDataTestBase.Mapper());
        resourceClaimsForClaimSet = getResourcesByClaimSetIdQuery.AllResources(testClaimSet.ClaimSetId).ToList();

        var resultResourceClaim1 =
            resourceClaimsForClaimSet.Single(x => x.Id == overrideModel.ResourceClaimId);

        resultResourceClaim1.AuthStrategyOverridesForCRUD.Count.ShouldBe(1);
        resultResourceClaim1.AuthStrategyOverridesForCRUD[0].ActionName.ShouldBe("Create");
        resultResourceClaim1.AuthStrategyOverridesForCRUD[0].AuthorizationStrategies.First().AuthStrategyName.ShouldBe("TestAuthStrategy1");

        var resultResourceClaim2 =
            resourceClaimsForClaimSet.Single(x => x.Id == testResource2ToNotEdit.ResourceClaimId);

        resultResourceClaim2.AuthStrategyOverridesForCRUD.ShouldBeEmpty();     
    }

    [Test]
    public void ShouldOverrideAuthorizationStrategiesForSpecificResourcesOnClaimSet()
    {
        InitializeData(out var testClaimSet, out var appAuthorizationStrategies, out var testResource1ToEdit, out var testResource2ToNotEdit);

        var overrides = new List<int>();
        if (appAuthorizationStrategies != null)
        {
            foreach (var appAuthorizationStrategy in appAuthorizationStrategies)
            {
                overrides.Add(appAuthorizationStrategy.AuthorizationStrategyId);
            }
        }
        var overrideModel = new OverrideAuthStrategyOnClaimSetModel
        {
            ResourceClaimId = testResource1ToEdit.ResourceClaimId,
            ClaimSetId = testClaimSet.ClaimSetId,
            ActionName = "Create",
            AuthStrategyIds = overrides
        };

        List<ResourceClaim> resourceClaimsForClaimSet = null;

        using var securityContext = TestContext;
        var command = new OverrideDefaultAuthorizationStrategyCommand(securityContext);
        command.ExecuteOnSpecificAction(overrideModel);
        var getResourcesByClaimSetIdQuery = new GetResourcesByClaimSetIdQuery(securityContext, SecurityDataTestBase.Mapper());
        resourceClaimsForClaimSet = getResourcesByClaimSetIdQuery.AllResources(testClaimSet.ClaimSetId).ToList();

        var resultResourceClaim1 =
            resourceClaimsForClaimSet.Single(x => x.Id == overrideModel.ResourceClaimId);

        resultResourceClaim1.AuthStrategyOverridesForCRUD.Count.ShouldBe(1);
        resultResourceClaim1.AuthStrategyOverridesForCRUD[0].ActionName.ShouldBe("Create");
        resultResourceClaim1.AuthStrategyOverridesForCRUD[0].AuthorizationStrategies.First().AuthStrategyName.ShouldBe("TestAuthStrategy1");

        var resultResourceClaim2 =
            resourceClaimsForClaimSet.Single(x => x.Id == testResource2ToNotEdit.ResourceClaimId);

        resultResourceClaim2.AuthStrategyOverridesForCRUD.ShouldBeEmpty();
    }

    private void InitializeData(out ClaimSet testClaimSet, out List<Security.DataAccess.Models.AuthorizationStrategy> appAuthorizationStrategies, out Security.DataAccess.Models.ResourceClaim testResource1ToEdit, out Security.DataAccess.Models.ResourceClaim testResource2ToNotEdit)
    {
        var testApplication = new Application
        {
            ApplicationName = "TestApplicationName"
        };

        Save(testApplication);

        testClaimSet = new ClaimSet
        {
            ClaimSetName = "TestClaimSet",
            Application = testApplication
        };
        Save(testClaimSet);

        appAuthorizationStrategies = SetupApplicationAuthorizationStrategies(testApplication).ToList();
        var parentRcNames = UniqueNameList("ParentRc", 2);

        var testResourceClaims = SetupParentResourceClaimsWithChildren(
            testClaimSet, testApplication, parentRcNames, UniqueNameList("Child", 1));

        SetupResourcesWithDefaultAuthorizationStrategies(
            appAuthorizationStrategies, testResourceClaims.ToList());

        testResource1ToEdit = testResourceClaims.Select(x => x.ResourceClaim)
            .Single(x => x.ResourceName == parentRcNames.First());
        testResource2ToNotEdit = testResourceClaims.Select(x => x.ResourceClaim)
            .Single(x => x.ResourceName == parentRcNames.Last());
    }
}
