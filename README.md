##Web API that gets WebHook/Notification on POST and enters it to DB ##
 1. Enter the data first to collection, then read from this collection (other thread), and enter it to DB
 2. We want to keep the order of the webhooks
 3. In order to prevent loss of data, think on mechanism that backups to file on restart


 Sample WebHook payload:
{
	"date":	"2022-01-01T02:01:30",
	"json":	"{
				\"a\":\"b\",
				\"c\":\"d\"
			}"
}