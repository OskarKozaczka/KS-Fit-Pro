using Plugin.BLE.Abstractions.EventArgs;

namespace KS_Fit_Pro.Source
{
    public class BeltController
    {
        BeltState currentBeltState;

        public readonly BLEConnector _bleConnector;
        private readonly BeltRequestReceiver _receiver;

        public BeltController(BLEConnector bleConnector, BeltRequestReceiver receiver)
        {
            _bleConnector = bleConnector;
            _receiver = receiver;
            //bleConnector.StatusCharacteristic.ValueUpdated += MessageReceived;
        }

        internal void MessageReceived(object sender, CharacteristicUpdatedEventArgs e)
        {
            currentBeltState = _receiver.GetBeltStateFromMessage(e.Characteristic.Value);
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
    }
}
