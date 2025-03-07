import { last } from "cypress/types/lodash";

class CheckSchoolDetails {
    
    public hasHeader(header : string): this
    {
        header = 'Check school details'

        cy.get('h1').contains(header)

        return  this;
    }

    public hasSchoolName(school : string) : this
    {
        school = "Plymouth Grove Primary School"

        cy.get('[data-cy="school-name"]').contains(school)

        return this;
    }

    public hasURN(URN : string) : this
    {
        URN = '105443'

        cy.get('.govuk-summary-list__value').eq(1).contains(URN)

        return this;
    }

    public hasLocalAuthority(localAuthority : string) : this
    {
        localAuthority = 'Manchester'

        cy.get('.govuk-summary-list__value').eq(3).contains(localAuthority)

        return this;
    }

    public hasSchoolType(schoolType : string) : this
    {
        schoolType = 'Community school'

        cy.get('.govuk-summary-list__value').eq(5).contains(schoolType)

        return this;
    }

    public hasFaithSchool(faithSchool : string) : this
    {
        faithSchool = 'Does not apply'

        cy.get('.govuk-summary-list__value').eq(7).contains(faithSchool)

        return this;
    }

    public hasOfstedRating(ofstedRating : string) : this
    {
        ofstedRating = 'Good'

        cy.get('.govuk-summary-list__value').eq(9).contains(ofstedRating)

        return this;
    }

    public hasLastInspection(lastInspection : string) : this
    {
        lastInspection = '16 January 2011'

        cy.get('.govuk-summary-list__value').eq(11).contains(lastInspection)

        return this;
    }

    public hasPFI(pfi : string) : this
    {
        pfi = 'Does not apply'

        cy.get('.govuk-summary-list__value').eq(13).contains(pfi)

        return this;
    }

    public clickContinue(): this
    {
        cy.contains('Continue').click()
    
        return this;
    }
    
    }
    
    const checkSchoolDetails = new CheckSchoolDetails();
    
    export default checkSchoolDetails;