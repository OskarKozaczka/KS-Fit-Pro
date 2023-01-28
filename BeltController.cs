using Plugin.BLE.Abstractions.EventArgs;

namespace KS_Fit_Pro
{
    internal class BeltController
    {
        BeltState currentBeltState;

        public readonly BLEConnector _connector;
        private readonly BeltRequestReceiver _receiver;

        public BeltController()
        {
            _connector = new BLEConnector(this);
            _receiver = new BeltRequestReceiver();
        }

        internal void MessageReceived(object sender, CharacteristicUpdatedEventArgs e)
        {
            this.currentBeltState = _receiver.HandleReceivedMessage(e.Characteristic.Value);
        }

        public async Task SetSpeed(int speed)
        {
            var message = BeltRequestHelper.GetEncodedMessage(EnBeltAction.CHANGE_SPEED, speed);
            //await _connector.SendMessage(message);
        }

        internal async Task Start()
        {
            var message = BeltRequestHelper.GetEncodedMessage(EnBeltAction.START, 1);
            //await _connector.SendMessage(message);
        }
    }
}
