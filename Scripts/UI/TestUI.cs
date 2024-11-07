using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Project_PropHunt.UI
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private Button clientButton;
        [SerializeField] private Button hostButton;

        private void Awake()
        {
            clientButton.onClick.AddListener(() => {
                NetworkManager.Singleton.StartClient();
            });

            hostButton.onClick.AddListener(() => {
                NetworkManager.Singleton.StartHost();
            });
        }
    }
}
