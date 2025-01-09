﻿using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Tests.Entities.SupportProject
{
    public class SupportProjectTests
    {
        private MockRepository mockRepository;


        public SupportProjectTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange Act
            var supportProject = CreateSupportProject();

            // Assert
            supportProject.Should().NotBeNull();
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SetAdviser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supportProject = CreateSupportProject();
            string assignedAdviserFullName = "testName";
            string assignedAdviserEmailAddress = "Test@Email.com";

            // Act
            supportProject.SetAdviser(
                assignedAdviserFullName,
                assignedAdviserEmailAddress);

            // Assert
            supportProject.AssignedAdviserFullName.Should().Be(assignedAdviserFullName);
            supportProject.AssignedAdviserEmailAddress.Should().Be(assignedAdviserEmailAddress);
            this.mockRepository.VerifyAll();
        }

        private static Domain.Entities.SupportProject.SupportProject CreateSupportProject(
            string schoolName = "Default School",
            string schoolUrn = "DefaultURN",
            string localAuthority = "Default Authority",
            string region = "Default Region",
            string createdBy = "Default Creator",
            DateTime createdOn = default)
        {
            return Domain.Entities.SupportProject.SupportProject.Create(
                 schoolName,
                 schoolUrn,
                 localAuthority,
                 region,
                 createdBy,
                 createdOn == default ? DateTime.UtcNow : createdOn);
        }
    }
}