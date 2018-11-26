namespace TspPsoDemo
{
    using System.Collections.Generic;

  

    public interface ITspParticle
    {
        List<ITspParticle> InformersList { get; set; }

        IRoute CurrentRoute { get; set; }

        IRoute LocalBestRoute { get; set; }

        IRoute PersonalBestRoute { get; set; }

        double Optimize(PsoAttributes psoAttributes);
    }
}