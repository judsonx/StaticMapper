using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMapper
{
  public class MapKey : IEquatable<MapKey>
  {
    public Type T0 { get; set; }
    public Type T1 { get; set; }

    public override int GetHashCode()
    {
      unchecked
      {
        var hash0 = T0.GetHashCode();
        var hash1 = T1.GetHashCode();
        return ((hash0 << 5) + hash0) ^ hash1;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      return Equals((MapKey)obj);
    }

    public bool Equals(MapKey other)
    {
      return T0 == other.T0 && T1 == other.T1;
    }
  }
}
