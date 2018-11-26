namespace TspPsoDemo
{
   

  public  interface ITspParticleFactory
    {
        ITspParticle GetTspParticle(IRoute route);
    }
}
