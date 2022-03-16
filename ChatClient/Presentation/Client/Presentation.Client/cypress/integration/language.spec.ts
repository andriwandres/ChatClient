describe('Language Switch', () => {

  beforeEach(() => {
    cy.visit('/');
  });

  it('should set english as the default language', () => {
    cy.contains('#language-name', 'English');
  });

  it('should switch language to german', () => {
    cy.get('app-language-selector #menu-trigger').click();
    cy.contains('app-language-item:nth-child(2)', 'German');

    cy.get('app-language-item:nth-child(2)').click();
    cy.contains('#language-name', 'Deutsch');
  });

});
