using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StrangeAttractorType { Lorenz, Dadras, Aizawa, Thomas, Chen, Lorenz83, Rossler, Halvorsen, RabinovichFabrikant, ThreeScrollUnifiedChaoticSystem, Sprott, FourWings }

public static class StrangeAttractorUtils
{
    public static StrangeAttractor GetStrangeAttractorFromType(StrangeAttractorType type)
    {
        StrangeAttractor strangeAttractor = new StrangeAttractor();

        switch (type)
        {
            case (StrangeAttractorType.Lorenz):
                strangeAttractor = new StrangeAttractorLorenz();
                break;
            case (StrangeAttractorType.Dadras):
                strangeAttractor = new StrangeAttractorDadras();
                break;
            case (StrangeAttractorType.Aizawa):
                strangeAttractor = new StrangeAttractorAizawa();
                break;
            case (StrangeAttractorType.Thomas):
                strangeAttractor = new StrangeAttractorThomas();
                break;
            case (StrangeAttractorType.Chen):
                strangeAttractor = new StrangeAttractorChen();
                break;
            case (StrangeAttractorType.Lorenz83):
                strangeAttractor = new StrangeAttractorLorenz83();
                break;
            case (StrangeAttractorType.Rossler):
                strangeAttractor = new StrangeAttractorRossler();
                break;
            case (StrangeAttractorType.Halvorsen):
                strangeAttractor = new StrangeAttractorHalvorsen();
                break;
            case (StrangeAttractorType.RabinovichFabrikant):
                strangeAttractor = new StrangeAttractorRabinovichFabrikant();
                break;
            case (StrangeAttractorType.ThreeScrollUnifiedChaoticSystem):
                strangeAttractor = new StrangeAttractorThreeScroll();
                break;
            case (StrangeAttractorType.Sprott):
                strangeAttractor = new StrangeAttractorSprott();
                break;
            case (StrangeAttractorType.FourWings):
                strangeAttractor = new StrangeAttractorFourWings();
                break;
        }

        return strangeAttractor;
    }
}