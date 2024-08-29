namespace RestWithASPNet.Data.Converter.Contract
{
    public interface IParser<O, D>
    {
        D Parser(O origem);
        List<D> Parser(List<O> origem);
    }
}
