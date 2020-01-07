Select * from kuzemkin.INFORMATION_SCHEMA.TABLES;
/*Create Table trades
(
	trade_id int IDENTITY(1,1),
	open_time datetime not null,
	close_time datetime,
	open_price decimal not null,
	close_price decimal,
	order_type char(4) check(order_type in ('Buy', 'Sell')),
	amount decimal not null,
	trade_state char check(trade_state in ('InProgress', 'Active', 'Close'))
	constraint pk_trades Primary Key (trade_id)
)*/
Select * from trades;
