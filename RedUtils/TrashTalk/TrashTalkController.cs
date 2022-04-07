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

        public TrashTalkController(IAction action)
        {
            CurrentAction = action;
        }

        public QuickChatSelection TrashTalk()
        {
            if (CurrentAction is Shot)
            {
                return QuickChatSelection.Compliments_WhatASave;
            }
            else return QuickChatSelection.Apologies_Sorry;
        }
    }
}
