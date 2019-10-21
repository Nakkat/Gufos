INSERT INTO TipoUsuario (Titulo) VALUES ('Administrador'), ('Aluno')
select * from TipoUsuario
INSERT INTO Usuario (Nome, Email, Senha, IdTipoUsuario) VALUES ('Administrador', 'adm@adm.com', '123',1), 
('Ariel', 'ariel@email.com', '123', 2)

INSERT INTO Localizacao (CNPJ, RazaoSocial, Endereco)
VALUES ('12345678912345', 'Escola SENAI de Informática', 'Rua Alameda Barão de Limeira, 529')

INSERT INTO Categoria (Titulo)
VALUES ('Desenvolvimento'),
('HTML + CSS'),
('Marketing')

INSERT INTO Eventos (Titulo, IdCategoria, AcessoLivre, DataEvento, IdLocalizacao)
VALUES ('C#', 2, 0, '2019-08-07T18:00:00', 1),
('Estrutura Semântica', 2, 1, GETDATE(), 1)

Select * from Eventos

INSERT INTO Presenca (IdEvento, IdUsuario, PresencaStatus)
VALUES (1, 2, 'AGUARDANDO'), (1,1, 'CONFIRMADO')