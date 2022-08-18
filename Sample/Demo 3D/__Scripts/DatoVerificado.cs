public struct DatoVerificado<TTipe>
{
    private TTipe _valor;
    private bool _verificado;

    public DatoVerificado(TTipe valor, bool verificado = false)
    {
        _valor = valor;
        _verificado = verificado;
    }

    public TTipe Valor
    {
        get => _valor;
        set
        {
            _valor = value;
            _verificado = true;
        }
    }

    public bool Verificado { get => _verificado; }

    public void Desactualizado()
    {
        _verificado = false;
    }
}
