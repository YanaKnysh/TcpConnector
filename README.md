# TcpConnector
Техническое задание:
Написать приложение, которое по TCP (порт 1000) принимает входящие соединения (может быть больше 1), 
если запрос состоит из команд из списка ниже, то обрабатываем согласно протоколу, в противном случае возвращаем  
"SOHUnknownCommandETB".
Список команд:
1) запрос печати: "SOHPrint<labelname>ETB", в ответ приложение через 1с отвечает "SOHPrintDoneETB". "labelname" 
считается корректным только "Label1", "Label2", если принято иное значение, программа должна вернуть "SOHWrongLabelETB" 
без задержек.
2) запрос статуса: "SOHGetStatusETB", в ответ приложение сразу должно вернуть текущее состояние: 
"SOHIdleETB" - ожидание, "SOHprintingETB" - если ожидается 1 секунда после запроса печати
Где SOH = стартовый байт со значением 0х01, ETB = конечный байт со значением 0х17
Все принятые команды и ответы выводить в консоль в формате: 
<Время с мс><ip>:<port> <направление> <команда/ответ>,
где
  ip - ip клиента (remote ip)
  port - порт клиента
  направление - "=>" сообщение от клиента, "<=" сообщение для клиента
  команда/ответ - понятно
По нажатию пробела корректно завершать все соединения и затем завершать приложение
