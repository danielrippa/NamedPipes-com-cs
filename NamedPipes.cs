using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace NamedPipes {

  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [Guid("376186C1-059F-4D23-804B-260788A735C2")]
  [ProgId("NamedPipes.Client")]

  public class Client {

    private NamedPipeClientStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    public void Connect(string name, int pipeDirection) {
      var direction = (PipeDirection)pipeDirection;
      stream = new NamedPipeClientStream(".", name, direction);
      stream.Connect();
      reader = new StreamReader(stream);
      writer = new StreamWriter(stream) { AutoFlush = true };
    }

    public string Read() {
      return reader.ReadLine();
    }

    public void Write(string message) {
      writer.WriteLine(message);
    }

    public void Disconnect() {
      if (stream != null) {
        reader.Close();
        writer.Close();
        stream.Close();
        stream.Dispose();
        stream = null;
      }
    }

    public bool IsConnected() {
      return stream != null && stream.IsConnected;
    }

  }

  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [Guid("E0D19F7C-B8D4-43C4-B6D2-629F1D405796")]
  [ProgId("NamedPipes.Server")]

  public class Server {

    private NamedPipeServerStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    public void Start(string name, int pipeDirection) {
      var direction = (PipeDirection)pipeDirection;
      stream = new NamedPipeServerStream(name, direction);
      stream.WaitForConnection();
      reader = new StreamReader(stream);
      writer = new StreamWriter(stream) { AutoFlush = true };
    }

    public void Write(string message) {
      writer.WriteLine(message);
    }

    public string Read() {
      return reader.ReadLine();
    }

    public void Stop() {
      if (stream != null) {
        reader.Close();
        writer.Close();
        stream.Close();
        stream.Dispose();
        stream = null;
      }
    }

    public bool IsConnected() {
      return stream != null && stream.IsConnected;
    }

  }

}