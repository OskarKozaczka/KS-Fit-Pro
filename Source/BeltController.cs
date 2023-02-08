using KS_Fit_Pro.Models;
using Plugin.BLE.Abstractions.EventArgs;

namespace KS_Fit_Pro.Source
{
    public class BeltController
    {
        public BeltState CurrentBeltState;
        public BeltState? LastBeltState;

        public readonly BLEConnector _bleConnector;
        private readonly BeltRequestReceiver _receiver;

        public BeltController(BLEConnector bleConnector, BeltRequestReceiver receiver)
        {
            _bleConnector = bleConnector;
            _receiver = receiver;
            _bleConnector.OnMessageReceived += MessageReceived;
        }

        internal void MessageReceived(object sender, CharacteristicUpdatedEventArgs e)
        {
            CurrentBeltState = _receiver.GetBeltStateFromMessage(e.Characteristic.Value);
            if (LastBeltState != null) CurrentBeltState += LastBeltState;
        }

        public async Task SetSpeed(int speed)
        {
            var message = BeltRequestHelper.GetEncodedMessage(EnBeltAction.CHANGE_SPEED, speed);
            await _bleConnector.SendMessage(message);
        }

        internal async Task Start()
        {
            var message = BeltRequestHelper.GetEncodedMessage(EnBeltAction.START, 1);
            await _bleConnector.SendMessage(message);
        }

        internal void ResetState()
        {
            CurrentBeltState = new BeltState();
        }

        internal void SaveLastState()
        {
            LastBeltState = CurrentBeltState;
        }

        internal async Task SetMode(BeltMode mode)
        {
            var message = BeltRequestHelper.GetEncodedMessage(EnBeltAction.SWITCH_MODE, (int)mode);
            await _bleConnector.SendMessage(message);
        }
    }
}
