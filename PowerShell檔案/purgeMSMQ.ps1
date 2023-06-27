
# 載入System.Messaging組件
Add-Type -AssemblyName System.Messaging
##------------------------------------------------------------------------------------------

 #WriteLog "開始-執行"
  while ($true) {
    $randomNumber = Get-Random -Minimum 1000 -Maximum 9999
    Write-Host "密碼："$randomNumber
    $input = Read-Host "請輸入螢幕密碼確定執行「刪除所有MSMQ佇列訊息」"
    if($input -eq $randomNumber){
     Write-Host "開始執行"
    # 取得本機的MSMQ柱列
    $queues = [System.Messaging.MessageQueue]::GetPrivateQueuesByMachine(".")
        foreach ($queue in $queues) {
           
            $queuePath = ".\"+$($queue.QueueName)
            $queue = New-Object -TypeName System.Messaging.MessageQueue -ArgumentList $queuePath       
          
            $messageCount = $queue.GetAllMessages().Count
            Write-Host "佇列名稱："$queuePath "訊息數量"：$messageCount
            #WriteLog "佇列名稱："$queuePath "訊息數量"：$messageCount
            # 讀取並刪除訊息
            $queue.Purge()
            Write-Host "刪除成功"
            #WriteLog "刪除成功"
            # 關閉佇列連線
            $queue.Dispose()
            
        }
        Write-Host "-----------------------------------"
        Write-Host "執行完成!"
        break
    }
    else{
        Write-Host "輸入錯誤，請重新輸入"
    }
  }
 
  #WriteLog "結束-執行" 




function WriteLog($LogString){
    $TimeLog = (Get-Date).toString("yyyy/MM/dd HH:mm:ss")
    $LogMessage = "$TimeLog $LogString"
    $LogMessage | Out-File -FilePath ".\PurgeMSMQlog_.txt" -Encoding utf8
}
