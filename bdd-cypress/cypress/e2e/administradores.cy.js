/// <reference types="cypress" />
const host = Cypress.env('HOST');

describe('Administradores', () => {

  beforeEach(() => {
    cy.visit(`${host}/home/truncate`)
  })

  it('Cadastro de Administradores', () => {
    cy.visit(`${host}/administradores/Create`)
    let email = 'johndoe@example.com';

    cy.get('form[action="/Administradores/Create"]').within(() => {

      // Preenche os campos do formulário com informações fictícias
      cy.get('input#Nome').type('John Doe');
      cy.get('input#Email').type(email);
      cy.get('input#Senha').type('123456');
      cy.get('input[name="csenha"]').type('123456');

      cy.get('input[type="submit"]').should('not.be.disabled').click();
    });

    cy.get('table.table tbody tr td:nth-child(2)').should('contain', email);
  })


  it('Atualizar Administrador', () => {
    cy.visit(`${host}/administradores/Create`)
    let email = 'johndoe@example.com';

    cy.get('form[action="/Administradores/Create"]').within(() => {

      // Preenche os campos do formulário com informações fictícias
      cy.get('input#Nome').type('John Doe');
      cy.get('input#Email').type(email);
      cy.get('input#Senha').type('123456');
      cy.get('input[name="csenha"]').type('123456');

      cy.get('input[type="submit"]').should('not.be.disabled').click();
    });

    cy.get('table.table tbody tr td:nth-child(2)').contains('johndoe@example.com')
      .parent()
      .find('td:last-child a:first-child')
      .click();
      
      let nome = 'Danilo Aparecido'
      cy.get('form').within(() => {

        // Preenche os campos do formulário com informações fictícias
        cy.get('input#Nome').clear();
        cy.get('input#Nome').type(nome);

        cy.get('input#Email').clear();
        cy.get('input#Email').type(email);

        cy.get('input#Senha').clear();
        cy.get('input#Senha').type('123456');
  
        cy.get('input[type="submit"]').should('not.be.disabled').click();
      });

      cy.get('table.table tbody tr td:nth-child(1)').should('contain', nome);
  })


  it('Excluir Administrador', () => {
    cy.visit(`${host}/administradores/Create`)
    let email = 'johndoe@example.com';

    cy.get('form[action="/Administradores/Create"]').within(() => {

      // Preenche os campos do formulário com informações fictícias
      cy.get('input#Nome').type('John Doe');
      cy.get('input#Email').type(email);
      cy.get('input#Senha').type('123456');
      cy.get('input[name="csenha"]').type('123456');

      cy.get('input[type="submit"]').should('not.be.disabled').click();
    });

    cy.get('table.table tbody tr td:nth-child(2)').contains('johndoe@example.com')
      .parent()
      .find('td:last-child a:nth-child(3)')
      .click();

      cy.get('form').within(() => {
        cy.get('input[type="submit"]').should('not.be.disabled').click();
      });

      cy.get('table.table tbody tr').should('not.exist');
  })
  
})
