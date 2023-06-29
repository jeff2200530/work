select id,name,
case(date)
	when '1' then '超級喜歡'
	when '2' then '喜歡'
	when '3' then '普通'
	when '4' then '不喜歡'
	else '不知道' 
	end as '喜歡程度'
	from testTable