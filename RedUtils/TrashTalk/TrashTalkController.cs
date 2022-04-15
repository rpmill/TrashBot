using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rlbot.flat;

namespace RedUtils.TrashTalk
{
    public class TrashTalkController
    {
        private Car Me { get; set; }
        private int OurScore { get; set; }
        private int TheirScore { get; set; }
        private int Team { get; set; }
        private bool WeScored { get; set; }
        private bool TheyScored { get; set; }
        private bool BotScored { get; set; }
        private bool BotSaved { get; set; }
        private int DemoCounter { get; set; }
        private bool Demoed { get; set; }

        public TrashTalkController()
        {
        }

        public int TrashTalk()
        {

            int _trashTalkMessage = 99;

            // what states trigger a trashtalk comment? demoed, scoring, saves

            // generate 

            // demoed
            if (Demoed)
            {
                _trashTalkMessage = (int)QuickChatSelection.Reactions_Wow;
            }
            else if (WeScored && (OurScore > TheirScore))
            {
                switch (BotScored)
                {
                    case true:
                        _trashTalkMessage = (int)QuickChatSelection.Custom_Toxic_WasteCPU;
                        break;
                    case false:
                        _trashTalkMessage = (int)QuickChatSelection.Compliments_WhatASave;
                        break;
                }

            }
            else if (WeScored)
            {
                _trashTalkMessage = (int)QuickChatSelection.Custom_Excuses_Lag;
            }
            else if (TheyScored && (OurScore > TheirScore))
            {
                _trashTalkMessage = (int)QuickChatSelection.Apologies_NoProblem;
            }
            else if (TheyScored)
            {
                _trashTalkMessage = (int)QuickChatSelection.Custom_Toxic_404NoSkill;
            }
            else if (BotSaved)
            {
                _trashTalkMessage = (int)QuickChatSelection.Custom_Toxic_GitGut;
            }          

            UpdateFlags();
            return _trashTalkMessage;

        }

        public void Initialize(int team, Car me)
        {
            // set the starting properties
            // set the bot's team
            Team = team;
            Me = me;
            UpdateScores();
        }

        private int GetTrashTalk()
        {
            // get the max possible value of the enum
            int maxEnum = Enum.GetValues(typeof(QuickChatSelection)).Length;
            
            // returns a random enum
            return RandomNumber.GetRandomInt(0, maxEnum-1);
            
        }

        public void Update(Car me)
        {
            // update the scores
            UpdateScores();

            //update Me
            if (me.Goals > Me.Goals)
                BotScored = true;

            if (me.Saves > Me.Saves)
                BotSaved = true;

            if (me.IsDemolished && !Me.IsDemolished)
                UpdateDemos();


            Me = me;

            // update Toxcicity
        }

        private void UpdateScores()
        {
            int _ourScore = Game.Scores[Team];
            int _theirScore = Game.Scores[1 - Team];

            if (_ourScore > OurScore)
                WeScored = true;
            else if (_theirScore > TheirScore)
                TheyScored = true;

            OurScore = _ourScore;
            TheirScore = _theirScore;
        }

        private void UpdateFlags()
        {
            // resets the bools used to determine if an action took place
            WeScored = false;
            TheyScored = false;
            BotScored = false;
            BotSaved = false;
            Demoed = false;
        }

        private void UpdateDemos()
        {
            DemoCounter += 1;
            Demoed = true;
        }
    }
}
