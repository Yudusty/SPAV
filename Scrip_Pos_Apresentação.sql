--------------------------------------------------------------------------------
--Criando o banco de dados
--------------------------------------------------------------------------------
create database Inter_4
go
--------------------------------------------------------------------------------
--Acessar o Banco de Dados
--------------------------------------------------------------------------------
use Inter_4
go
--------------------------------------------------------------------------------
-- 1) Criando as Tabelas do Banco
--------------------------------------------------------------------------------
 
-- tabela: Pessoas
create table Pessoas
(
    idPessoa int not null primary key identity,
    nome     varchar(50) not null,
    email    varchar(100) not null unique,
    senha    varchar(100) not null,
    dataNasc date not null
)
go
 
select * from Pessoas
go
 
-- tabela: Empresas
create table Empresas
(
    idEmpresa int not null primary key references Pessoas(idPessoa),
    CNPJ      varchar(20) not null
)
go
 
select * from Empresas
go
 
-- tabela: funcionarios
create table Colaboradores
(
    idColaborador int not null primary key references Pessoas(idPessoa),
	empresaid int not null references Empresas(idEmpresa),
    salario decimal(10,2) not null,
    cpf     varchar(14) not null unique
)
go
 
select * from Colaboradores
go
 
-- tabela: Cliente
create table Clientes
(
    idCliente int not null primary key references Pessoas(idPessoa),
    cpf varchar(14) not null unique
)
go
 
select * from Clientes
go
 
-- tabela: Pedidos
create table Pedidos
(
    idPedido int not null primary key identity,
    horario date not null,
    mesa int not null,
	valor decimal(10, 2) not null,
	status varchar(20) not null,
    colaboradorid int null references Colaboradores (idColaborador),
	clienteid int not null references Clientes (idCliente)
)
go
 
select * from Pedidos
go
 
-- tabela: Produtos
create table Produtos
(
    idProduto int not null primary key identity,
    empresaid int not null references Empresas(idEmpresa),
    nome varchar (100) not null,
    descricao varchar(200) not null,
    qtd int not null    check(qtd >= 0),
    valor decimal(10,2)
)
go
 
select * from Produtos
go

  
-- tabela: Produtos_Pedidos
create table Produtos_Pedidos
(
    pedidoid    int not null references Pedidos(idPedido),
    produtoid   int not null references Produtos(idProduto),
    qtd         int not null,
    primary key(pedidoid, produtoid)
)
go
 
select * from Produtos_Pedidos
go
 
--------------------------------------------------------------------------------
--2) Comecando as criação das procedures
--------------------------------------------------------------------------------
 
-- Procedure 1 -- Cadastro de Cliente
create procedure sp_cadCliente
(
    --parametros recebidos
      @nomeCli varchar(50), @emailCli varchar(100), @senhaCli varchar(100),
      @NascCli date, @CPFCli varchar(14)
)
as
begin
        --cadastrar os dados das pessoas
        insert into Pessoas(nome, email, senha, dataNasc)
        values (@nomeCli, @emailCli, @senhaCli, @NascCli)
 
        --declarando uma variavel para receber o ultimo id
        declare @idCli int
        --Setando o ultimo id para a variavel recem criada
        set @idCli = @@IDENTITY
 
        --insert na tabela clientes
        insert into Clientes(idCliente, cpf)
        values (@idCli, @CPFCli)
end
go
 
-- Fim Procedure 1 -- Cadastro de Cliente
 
-- Procedure editando clientes
create procedure sp_ediCli
(
    --parametros recebidos
    @nomeCli varchar(50), @emailCli varchar(100), @senhaCli varchar(100), @idCliente int
)
as
begin
    --Update na tabela Pessoas
    Update Pessoas set nome = @nomeCli, email = @emailCli, senha = @senhaCli
    where Pessoas.idPessoa = @idCliente
end
go
 
--Procedure deletando Clientes
create procedure sp_DelCli
(
    --parametros recebidos
    @idCli int
)
as
begin
	
	delete from Produtos_Pedidos where Produtos_Pedidos.pedidoid = @idCli

	delete from Pedidos where Pedidos.clienteid = @idCli

    --delete na tabela Clientes
    delete from Clientes where Clientes.idCliente = @idCli
     
    --delete na tabela pessoas
    Delete from Pessoas where Pessoas.idPessoa = @idCli
 
end
go
 
-- Procedure 2 -- Cadastro de Funcionario
create  procedure sp_cadColaboradores
(
    --parametros recebidos
    @nomeCol    varchar(50), @CPFCol varchar(14), @emailCol varchar(100),
    @senhaCol varchar(100), @salario decimal(10,2), @dataNasc date, @idEmpresa int
)
as
begin
    --cadastrar os dados das pessoas
    insert into Pessoas (nome,email, senha, dataNasc)
    values (@nomeCol, @emailCol, @senhaCol, @dataNasc)

	declare @idColaborador int
	set @idColaborador = @@IDENTITY

	--cadastrar os dados das colaboradores
	insert into Colaboradores (idColaborador,empresaid, salario, cpf)
	values (@idColaborador,@idEmpresa, @salario, @CPFCol)
end
go


 
--Procedure editar colaborador
create procedure sp_ediCola
(
    --parametros recebidos
    @idCol int,@nomeCol varchar(50), @emailCol varchar(100),
    @senhaCol varchar(100), @salario decimal(10,2)
)
as
begin
    --update na tabela pessoas
    Update Pessoas set nome = @nomeCol, email = @emailCol, senha = @senhaCol
    where Pessoas.idPessoa = @idCol
 
    --Update na tabela Clientes
    Update Colaboradores set salario = @salario
end
go

 
create procedure sp_DelCol
(
    --parametros recebidos
    @idCol int
)
as
begin
 
    --delete na tabela Clientes
    delete from Colaboradores where Colaboradores.idColaborador = @idCol
     
    --delete na tabela pessoas
    Delete from Pessoas where Pessoas.idPessoa = @idCol
 
 
end
go
 
 
select * from Colaboradores
select * from Pessoas
go
 
-- Procedure 3 -- Cadastro de Empressas
create procedure sp_cadEmpresa
(
    -- parametros recebidos
    @nomeEmpre  varchar(50), @emailEmpre varchar(100),
    @senhaEmpre varchar(100), @CNPJ varchar(14), @datnasc date
)
as
begin
    -- insert na tabela pessoas
    insert into Pessoas(nome, email, senha, dataNasc)
    values (@nomeEmpre, @emailEmpre, @senhaEmpre, @datnasc)
    -- declarando uma variavel para receber o ultimo id criado
    declare @idEmpresa int
    -- fazendo essa variavel receber o ultimo id que foi criado
    set @idEmpresa = @@IDENTITY
    -- fazendo o insert na tabela empressas
    insert into Empresas(idEmpresa, CNPJ)
    values (@idEmpresa, @CNPJ)
end
go
 
 
--Procedure para editar empresa
create procedure sp_ediEmp
(
    --paremetros recebidos
    @idEmpresa int, @nomeEmpre varchar(50), @emailEmpre varchar(100), @senhaEmpre varchar (100)
)
as
begin
    --Update na tabela pessoas
    Update Pessoas set nome = @nomeEmpre, email = @emailEmpre, senha = @senhaEmpre
    where Pessoas.idPessoa = @idEmpresa
end
go
 
 
create procedure sp_DelEmp
(
    --parametros recebidos
    @idEmp int
)
as
begin
	
	delete from Colaboradores where Colaboradores.empresaid = @idEmp

    --delete na tabela Clientes
    delete from Empresas where Empresas.idEmpresa = @idEmp
     
    --delete na tabela pessoas
    Delete from Pessoas where Pessoas.idPessoa = @idEmp
 
 
end
go
 
 
--Procedure para cadastrar produtos
create procedure sp_CadProduto
(
    --paremetros recebidos
    @nome varchar(100),@descricao varchar(200), @qtd int , @valor decimal(10,2), @idEmpresa int
)
as
begin
    --insert na tabela Produtos
    insert into Produtos (nome, descricao, qtd, valor, empresaid)
    values (@nome, @descricao, @qtd, @valor, @idEmpresa)
end
go
 

--procedure para Editar os produtos
create procedure sp_ediProd
(
    --parametros recebidos
    @idprod int , @descricao varchar (50), @qtd int, @valor decimal(10,2)
 
)
as
begin
    --update na tabela Produtos
    Update Produtos set descricao = @descricao, qtd = @qtd, valor = @valor
    where Produtos.idProduto = @idprod
end
go
 
 
select * from Produtos
go
 

 
select * from Pedidos
go
 
--Procedure para editar pedidos
create procedure sp_ediPed
(
    --parametros recebidos
    @pedidoiD int , @mesa int
)
as
begin
    update Pedidos set mesa = @mesa
    where  Pedidos.idPedido = @pedidoid
end
go
 
--Teste de execução
--exec sp_ediPed 1,6,23.00, 5
--go
create Procedure delPed
(
	@idPed int
)
as
begin
 
    Delete from Produtos_Pedidos where Produtos_Pedidos.pedidoid = @idPed

    Delete from Pedidos where Pedidos.idPedido = @idPed
     
end
go



create procedure Produtos_Pedidos_1
(
     --parametros recebidos
        @ProdutoId int,  @mesa int, @QTD int, @clienteid int
)
As
Begin

		Declare @Pedidoid int
		Declare @valor decimal(10,2)
		---- obter o preco do produto e qtd
		Select @valor = valor
		From Produtos where idProduto = @ProdutoId

		Declare @status varchar(20)
		set @status = 'Aguardando'

		Select  @Pedidoid = idPedido From Pedidos
		Where clienteid = @clienteid and status = 'Aguardando'
		
		If @Pedidoid is null
		Begin
			Insert into pedidos (horario, mesa, valor, status, clienteid)
			Values(SYSDATETIME(), @mesa, @valor * @QTD, @status, @clienteid)

			Set @Pedidoid  =  SCOPE_IDENTITY()
		End
                 
		---Insert na tabela Produtos_Pedidos
		Insert into Produtos_Pedidos (pedidoid, produtoid, qtd)
		Values (@PedidoId,@ProdutoId, @QTD)

		---atualiza estoque
		Update Produtos set qtd = qtd - @qtd
		Where idProduto= @ProdutoId
End
Go

create procedure delCol
(
	@idColaborador int
)
as
begin
	
	Delete Colaboradores where Colaboradores.idColaborador = @idColaborador

	Delete Pedidos where Pedidos.colaboradorid = @idColaborador

	Delete Pessoas where Pessoas.idPessoa = @idColaborador
end
go


select * from Produtos_Pedidos

select * from pedCli

select * from Pedidos

select * from Produtos

select * from Pessoas

select * from Clientes


create procedure ediPedCol
(
	@idpedido int, @colaboradorid int, @status varchar(20)
)
as
Begin
	Update Pedidos Set colaboradorid = @colaboradorid, status = @status
	Where idPedido = @idpedido
End
go

select * from Pessoas
--------------------------------------------------------------------------------
--3) Insert em cada tabela
--------------------------------------------------------------------------------
---Tabela Pessoas/Clientes
exec sp_cadCliente 'Rubens','rubens@gmail.com', 'rubens123', '2003-04-04','111.111.111-11'
go
exec sp_cadCliente 'Paulo Roberto','roberto@gmail.com', 'roberto123', '2003-05-05','222.222.222-22'
go
exec sp_cadCliente 'Roberto Paulo','paulo@gmail.com', 'paulo123', '2003-04-04','333.333.333-33'
go
 
select * from Pessoas
select * from Colaboradores
 
 --Tabela Pessoas/Empresas
exec sp_cadEmpresa 'Fatec Rio Preto','fatec@gmail.com', 'fatec123', '777.777.777-77', '2009-12-12'
go
exec sp_cadEmpresa 'Field Control','field@gmail.com', 'field123', '888.888.888-88', '2008-03-12'
go
exec sp_cadEmpresa 'Shift','shift@gmail.com', 'shift123', '999.999.999-99', '2007-02-12'
go
 
--Tabela Pessoas/Colaborador
exec sp_cadColaboradores'Matheus Grinffo','444.444.444-44','matheus.grinffo@fatec.sp.gov.br','matheus1234',3000, '2004-09-12', 4
go
exec sp_cadColaboradores'Wérik Nascimento','555.555.555-55','werik.nascimento@fatec.sp.gov.br','werik1234',2000,'2005-09-12',5
go
exec sp_cadColaboradores'Pedro Arakaki','666.666.666-66','pedro.arakaki@fatec.sp.gov.br','pedro1234',3000,'2006-11-12',6
go

select * from Colaboradores
select * from Pessoas
 

select * from Empresas
select * from Pessoas
 
--Tabela Produtos
exec sp_CadProduto 'Espaguete Com Almodenhas','Massa fina italiana com esferas feitas de carne bovina',23,4.56, 4
go
exec sp_CadProduto 'Suchi','Peixe requintado de água  doce, envolta em uma planta muito bem cuidada, originada  ',50,23.00, 5
go
exec sp_CadProduto 'Pastel de Frango','Frango envolto em uma massa asiatica frita no óleo de produtos naturais',100,16.50, 6
go
 
--Tabela Pedidos
exec Produtos_Pedidos_1 1,2,2,4
go
exec Produtos_Pedidos_1 2,3,3,5
go
exec Produtos_Pedidos_1 3,4,5,6
go
 
select * from Pedidos
 
--------------------------------------------------------------------------------
--4) Views COM JOIN
--------------------------------------------------------------------------------
--View Clientes
create view v_Clientes
as
    select P.idPessoa [IdCliente], P.nome [Nome do Cliente], P.email [Usuario], P.senha [Senha], P.dataNasc [Data Nascimento], C.CPF [CPF do Cliente]
    from Pessoas as P inner join Clientes as C on P.idPessoa = C.idCliente
go
 
--Teste de execução
select * from v_Clientes
go
 
--View Colaborador
create view v_Colaborador
as
    select P.idPessoa[IdColaborador], P.nome [Nome do Colaborador], P.email [Usuario], P.senha [Senha], P.dataNasc [Data de Fundação],  C.cpf [CPF], E.idEmpresa[IdEmpresa]
    from Pessoas as P inner join Colaboradores as C on P.idPessoa =C.idColaborador inner join Empresas as E on C.empresaid = E.idEmpresa
go
 
--Teste de execução
select * from v_Colaborador
go
 
--View Empresas
create view v_Empresas
as
    select P.idPessoa [IdEmpresa], P.nome [Nome da Empresa], P.email [Usuario], P.senha [Senha], P.dataNasc [Data da Fundação], E.CNPJ [CNPJ]
    from Pessoas P inner join Empresas E on P.idPessoa = E.idEmpresa
go
 
--Teste de execução
select * from v_Empresas
go

 --View para clientes pedidos
create view pedCli
 as
	select P.clienteid[IdCliente], P.idPedido[IdPedido], P.horario [Horario do Pedido], P.mesa [n mesa], sum(Pr.valor * PP.qtd) [Valor do Pedido], P.status[Status]			
	from  Pedidos P
	      INNER JOIN
		  Clientes C on C.idCliente = P.clienteid
		  INNER JOIN
		  Produtos_Pedidos PP on P.idPedido = PP.pedidoid
		  INNER JOIN
		  Produtos Pr on PP.produtoid = Pr.idProduto
	group by  P.clienteid, P.idPedido, P.horario, P.mesa, P.status;
go

select * from pedCli
go


--View Colaborador empresa
create view EmpColaborador
as
	select E.idEmpresa[IdEmpresa], P.idPessoa [IdColaborador], P.nome [Nome do Colaborador], P.email [email do colaborador], P.senha [senha do colaborador], P.dataNasc [data do nascimento], C.salario [Salario Colaborador]
	from Colaboradores C
	INNER JOIN
	Pessoas P on C.idColaborador = P.idPessoa
	INNER JOIN
	Empresas E on C.empresaid = E.idEmpresa

go		

--teste de execução
select * from EmpColaborador

select * from Colaboradores

--drop database Inter_4
go

select * from Pessoas
