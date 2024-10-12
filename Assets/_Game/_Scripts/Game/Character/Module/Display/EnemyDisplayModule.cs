using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    public class EnemyDisplayModule : BaseDisplayModule
    {
        [SerializeField]
        SpriteRenderer alertSpriteRenderer;
        [SerializeField]
        GameObject alertObject;
        public void OnChangeAlertState(ALERT_STATE state)
        {
            switch (state)
            {
                case ALERT_STATE.NONE:
                    alertObject.SetActive(false);
                    break;
                case ALERT_STATE.START:
                    alertObject.SetActive(true);
                    alertSpriteRenderer.color = Color.white;
                    break;
                case ALERT_STATE.MED_ALERT:
                    alertObject.SetActive(true);
                    alertSpriteRenderer.color = Color.yellow;
                    break;
                case ALERT_STATE.ALERT:
                    alertObject.SetActive(true);
                    alertSpriteRenderer.color = Color.red;
                    break;                        
            }
        }
    }
}