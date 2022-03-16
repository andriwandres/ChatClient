describe('Landing Page', () => {

  beforeEach(() => {
    cy.visit('/');
  });

  it('should display the slogan', () => {
    cy.contains('Stay connected');
    cy.contains('with ng-messenger');
  });

});
