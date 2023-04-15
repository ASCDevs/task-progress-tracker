CREATE TABLE TasksTracker.dbo.tarefas(
	id int not null identity(1,1),
	id_tarefa VARCHAR(300),
	nm_tarefa VARCHAR(500),
	dt_pedido_tarefa DATETIME,
	dt_inicio_tarefa DATETIME,
	dt_fim_tarefa DATETIME,
	status varchar(500)
)

SELECT 
	nm_tarefa, 
	dt_inicio_tarefa,
	dt_fim_tarefa, 
	DATEDIFF(SECOND,dt_inicio_tarefa,dt_fim_tarefa) as tempo_execucao,
	DATEDIFF(SECOND,dt_pedido_tarefa,dt_inicio_tarefa) as tempo_espera,
	DATEDIFF(SECOND,dt_pedido_tarefa,dt_fim_tarefa) as tempo_total
FROM TasksTracker.dbo.tarefas
