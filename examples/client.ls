client = new ActiveXObject 'NamedPipes.Client'

messages = <[ hi bye ]>

client.Connect 'conversation', 3

for message in messages

  if !client.IsConnected
    break

  client.Write message
  response = client.Read!

  WScript.Echo "response: #response"

try client.Disconnect!


