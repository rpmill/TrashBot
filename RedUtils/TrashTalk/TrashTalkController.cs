using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rlbot.flat;

namespace RedUtils.TrashTalk
{
    class TrashTalkController
    {
        public IAction CurrentAction { get; set; }
        public int TheirScore { get; set; }
        public Car Me { get; set; }

        public TrashTalkController()
        {

        }

        public int TrashTalk(IAction action, Car me, int theirScore)
        {
            // check if the CurrentAction is null
            PopulateProps(action, me);

            int _holdChat;

            // this won't send for the first shot, but subsequent shots it should
            if (action is Shot && action != CurrentAction)
            {
                _holdChat = GetTrashTalk();
            }
            else if (Me.IsDemolished)
            {
                _holdChat = (int)QuickChatSelection.Reactions_Wow;
            }
            else if (theirScore > TheirScore)
            {
                _holdChat = (int)QuickChatSelection.Compliments_NiceShot;
            }
            else _holdChat = 99;

            ResetProps(action, me, theirScore);

            return _holdChat;

        }

        private int GetTrashTalk()
        {
            // get the max possible value of the enum
            int maxEnum = Enum.GetValues(typeof(QuickChatSelection)).Length;
            
            // returns a random enum
            return RandomNumber.GetRandomInt(0, maxEnum-1);
            
        }

        private void ResetProps(IAction action, Car me, int theirScore)
        {
            CurrentAction = action;
            Me = me;
            TheirScore = theirScore;
        }

        private void PopulateProps(IAction action, Car me)
        {
            CurrentAction = (CurrentAction == null) ? action : CurrentAction;
            Me = (Me == null) ? me : Me;
        }
    }
}
