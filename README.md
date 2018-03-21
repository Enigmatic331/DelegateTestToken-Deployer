# DelegateTestToken-Deployer

This test allows access to an ERC20 deployed on address 0x006A9f55eC7B134C59E9cBa6f5fD41cdeC1991f9, with a signed transfer function which allows function call delegation to a third party to pay for gas.

Test defaults to 1% transfer fees, which will be calculated on top of the total token transfer sum.

Requires:
1. **Private key of token holder**
   >To sign transaction being deployed so tokens are transferred from this account to another. This is meant to be ran on the client end without ever needing the private keys to be transmitted out of the client device.
   
   >Use token holder 0x9c51a4e541012a239a23a51ce1c53f040429981e (private key: adfc7f24351cd5519df94fe8b802650bedca6a414531efbf3363a078304abd2d) to test.


2. **To address (where the tokens should be sent to).**
   >The token receiving address.


3. **Amount (number of tokens to send).**
   >Number of tokens to send. Numeric.


4. **Deployer Private Key**
   >Private keys to the deploying address. This address will execute the signed transfer, and will require to hold some ethers to pay for gas.
