namespace TransOil.DataContext.EntityModels;

public abstract class TransformerCounterBase : CounterBase
{
    public virtual double TranformerRatio { get; set; }
}
