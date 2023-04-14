CREATE TABLE TasksTracker.dbo.tarefas(
	id int not null identity(1,1),
	id_tarefa VARCHAR(300) null UNIQUE,
	nm_tarefa VARCHAR(500),
	dt_pedido_tarefa DATE,
	dt_inicio_tarefa DATE,
	dt_fim_tarefa DATE,
	status varchar(500)
)