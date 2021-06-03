using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private HumanPosition relatedHumanPosition;
    [SerializeField] [TagSelector] private string trayTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(trayTag))
        {
            Human humanOnPosition = relatedHumanPosition.GetHumanOnPosition();
            Tray currentTray = other.gameObject.GetComponent<Tray>();

            if (humanOnPosition != null)
            {
                List<string> trayFigureTypes = other.gameObject.GetComponent<Tray>().GetTrayFigureTypes();
                List<string> humanRequiredFigureTypes = humanOnPosition.GetRequiredFigureTypes();

                if (trayFigureTypes.Count == humanRequiredFigureTypes.Count)
                {
                    if (humanRequiredFigureTypes.All(trayFigureTypes.Contains))
                    {
                        currentTray.TakeTray();
                        humanOnPosition.MoveBack();
                    }
                }
            }
        }
        
    }
}
