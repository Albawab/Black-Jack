// <copyright file="ConsoleCommunicator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.SpelSpullen;

    public class ConsoleCommunicator : ICommunicate
    {
        public bool AskFichesInzetten(SpelerHand hand, int minWaarde, out Fiches fiches)
        {
            throw new NotImplementedException();
        }

        public bool AskWhichAction(SpelerHand hand, out Acties actie)
        {
            throw new NotImplementedException();
        }

        public void TellHand(SpelerHand hand, Meldingen melding)
        {
            switch (melding)
            {
                case Meldingen.ToonInzet:
                    this.ToonInzet(hand);
                    break;
            }
        }

        public void TellPlayer(Speler speler, Meldingen melding)
        {
            throw new NotImplementedException();
        }

        private void ToonInzet(SpelerHand hand)
        {
            Console.WriteLine("Uw inzet is {0}");

            //System.Reflection.Assembly otherAssembly = System.Reflection.Assembly.Load("BlackJackResources.dll");
//
            //System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("ResourceNamespace.myResources", otherAssembly);

            //string resource = Blackje
        }
    }
}
