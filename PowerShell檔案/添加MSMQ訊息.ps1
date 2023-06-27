# 引入 System.Messaging 命名空间
Add-Type -AssemblyName System.Messaging

# 设置队列的路径
$queuePath = ".\private$\queue"

# 创建消息队列对象
$messageQueue = New-Object System.Messaging.MessageQueue($queuePath)

# 创建消息内容
$messageBody = "Hello, MSMQ!"

# 创建消息对象
$message = New-Object System.Messaging.Message
$message.Body = $messageBody

# 发送消息到队列
$messageQueue.Send($message)
