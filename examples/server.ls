pipe = new ActiveXObject 'NamedPipes.Server'

pipe.Start 'conversation', 3

loop

  if !pipe.IsConnected!
    break

  message = pipe.Read!

  switch message

    | 'hi' => pipe.Write 'hello, stranger'

    else pipe.Write "#message back at you"

if pipe.IsConnected!

  pipe.Stop!
