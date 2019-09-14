using System.Collections.Generic;
using System.Linq;

namespace ChatApplication.BusinessLayer.BusinessObjects
{
  public class ConnectionMapping<T>
  {
    private readonly Dictionary<T, HashSet<string>> connections =
      new Dictionary<T, HashSet<string>>();

    public int Count
    {
      get
      {
        return this.connections.Count;
      }
    }

    public void Add(T key, string connectionId)
    {
      lock (this.connections)
      {
        if (!this.connections.TryGetValue(key, out HashSet<string> connectionsForKey))
        {
          connectionsForKey = new HashSet<string>();
          this.connections.Add(key, connectionsForKey);
        }

        lock (connectionsForKey)
        {
          connectionsForKey.Add(connectionId);
        }
      }
    }

    public IEnumerable<string> GetConnections(T key)
    {
      if (this.connections.TryGetValue(key, out HashSet<string> connectionsForKey))
      {
        return connectionsForKey;
      }

      return Enumerable.Empty<string>();
    }

    public void Remove(T key, string connectionId)
    {
      lock (this.connections)
      {
        if (!this.connections.TryGetValue(key, out HashSet<string> connectionsForKey))
        {
          return;
        }

        lock (connectionsForKey)
        {
          connectionsForKey.Remove(connectionId);

          if (connectionsForKey.Count == 0)
          {
            this.connections.Remove(key);
          }
        }
      }
    }
  }
}