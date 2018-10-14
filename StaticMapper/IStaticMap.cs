namespace StaticMapper
{
  public interface IStaticMap
  {
    object Map(object source);
    void Map(object source, object destination);
  }
}
