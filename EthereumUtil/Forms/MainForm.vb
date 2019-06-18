Imports Nethereum.Hex.HexTypes
Imports Nethereum.Web3
Imports Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions
Imports Nethereum.Signer.EthereumMessageSigner
Imports Nethereum.Util
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Text.RegularExpressions
Imports Nethereum.ABI.FunctionEncoding
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Contracts



Public Class MainForm
    Private pKey As New Nethereum.Signer.EthECKey("12ed9149b9202ccf46e7bdd856788cfc4e4c5b598ee445653f6bbccc7899ce84")
    Private account = New Nethereum.Web3.Accounts.Account(pKey)

    Private Async Sub testContract()
        Dim SHK As New Sha3Keccack

        ' disable controls
        btnExec.Enabled = False
        txtAmt.Enabled = False
        txtToAddress.Enabled = False
        txtDeployPrivKey.Enabled = False

        ' this can be a random account - We just need this to initialise the contract object and read the nonce
        Dim anEntirelyRandomAccount As New Nethereum.Signer.EthECKey("0000000000000000000000000000000000000000000000000000000000000001")
        Dim randomAccount = New Nethereum.Web3.Accounts.Account(anEntirelyRandomAccount)

        ' ABI of the deployed contract
        Dim abi = "[{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""distributeTokens"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""isTransferable"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""distributionAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signedTransferSig"",""outputs"":[{""name"":"""",""type"":""bytes4""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""balance"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""tokenOwner"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""tokens"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransferCheck"",""outputs"":[{""name"":""result"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_value"",""type"":""uint256""}],""name"":""burnSent"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""owner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""getNextNonce"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""}],""name"":""signedTransferHash"",""outputs"":[{""name"":""hash"",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""enableTransfers"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_setAddress"",""type"":""address""}],""name"":""setDistributionAddress"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""hash"",""type"":""bytes32""},{""name"":""sig"",""type"":""bytes""}],""name"":""ecrecoverFromSig"",""outputs"":[{""name"":""recoveredAddress"",""type"":""address""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""},{""name"":""_spender"",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":""remaining"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signingPrefix"",""outputs"":[{""name"":"""",""type"":""bytes""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""newOwner"",""type"":""address""}],""name"":""transferOwnership"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""burner"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_from"",""type"":""address""},{""indexed"":true,""name"":""_to"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_owner"",""type"":""address""},{""indexed"":true,""name"":""_spender"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]"
        Dim iweb3 = New Web3(randomAccount, "https://rinkeby.infura.io/")



        ' initialise the token delegated service classes
        ' the token delegated service class will inherit from the delegated signer service which acts as a generic delegable service
        Dim signedTransfer As New SignedTransferFunction
        Dim signedTransferCheck As New SignedTransferCheckFunction
        Dim transferFunction As New TransferFunction
        Dim GetNextNonce As New GetNextNonceFunction
        Dim tokenDelegatedService As New TokenDelegatedSignerService



        ' contract address - Get contract
        Dim tokenContractAddress = txtDeployedAt.Text
        Dim contractObj = iweb3.Eth.GetContractHandler(tokenContractAddress)

        ' address where we are sending tokens to
        Dim toAccount = txtToAddress.Text
        Dim fromAccount = txtHolderAddress.Text

        Dim tmpAmt As Int32 = Convert.ToInt32(txtAmt.Text)
        Dim tmpAmtFees As Int32 = tmpAmt * 0.01 'we take 1 % fees

        ' sending tokens
        Dim numTokens = Web3.Convert.ToWei(tmpAmt)
        Dim feeTokens = Web3.Convert.ToWei(tmpAmtFees)

        ' get nonce
        GetNextNonce.Owner = fromAccount
        Dim GetNextNonceHandler = contractObj.GetFunction(Of GetNextNonceFunction)
        Dim nonce = Await GetNextNonceHandler.CallAsync(Of Integer)(GetNextNonce)




        ' initialise delegated account (relayer)
        Dim delegatedprivateKey As New Nethereum.Signer.EthECKey(txtDeployPrivKey.Text)
        Dim delegatedAccount = New Nethereum.Web3.Accounts.Account(delegatedprivateKey)
        Dim delegateAddress = delegatedAccount.Address
        Dim feeAccount = delegatedAccount.Address

        ' prep the Delegable helper
        tokenDelegatedService.ContractAddress = tokenContractAddress

        ' prep TransferFunction
        transferFunction.To = toAccount
        transferFunction.Value = numTokens

        ' map transfer values to signedTransfer class
        Call tokenDelegatedService.MapDelegatedFunctionToSignedFunction(transferFunction, signedTransfer)

        ' prep SignedTransferFunction
        signedTransfer.TokenOwner = fromAccount
        signedTransfer.intNonce = nonce
        signedTransfer.Fee = feeTokens
        signedTransfer.FeeAccount = feeAccount
        Call tokenDelegatedService.GetSignedRequest(signedTransfer, pKey.GetPrivateKey)



        ''''''''''''''''''''''''''''''''''''''''
        ' test to ensure hashes are correct
        Dim hash = tokenDelegatedService.GetHashToSign(transferFunction, fromAccount, nonce, feeTokens)

        ' manually hashing to check, generate offchain hash
        Dim functionSig = ABITypedRegistry.GetFunctionABI(Of SignedTransferFunction).Sha3Signature
        Dim offchainHash = SHK.CalculateHashFromHex(functionSig.ToString, tokenContractAddress.PadLeft(40, "0"), fromAccount.PadLeft(40, "0"), toAccount.PadLeft(40, "0"),
                                               numTokens.ToString("x16").PadLeft(64, "0"), feeTokens.ToString("x16").PadLeft(64, "0"), nonce.ToString("x16").PadLeft(64, "0"))
        Debug.Print(IIf(hash = offchainHash, True, False))

        ' test to see if message is correctly signed
        Dim obj As New Nethereum.Signer.EthereumMessageSigner
        Dim test = obj.EcRecover(hash.HexToByteArray, signedTransfer.Sig.ToHex)
        Debug.Print(IIf(test = pKey.GetPublicAddress, True, False))
        ''''''''''''''''''''''''''''''''''''''''

        ' start checks using signedTransferCheck
        Dim proceed As Boolean
        signedTransferCheck.From = fromAccount
        signedTransferCheck.To = toAccount
        signedTransferCheck.TransferAmount = numTokens
        signedTransferCheck.Fee = feeTokens
        signedTransferCheck.intNonce = nonce
        signedTransferCheck.Sig = signedTransfer.Sig
        signedTransferCheck.FeeAccount = feeAccount
        Dim SignedTransferCheckHandler = contractObj.GetFunction(Of SignedTransferCheckFunction)
        Dim result = Await SignedTransferCheckHandler.CallAsync(Of String)(signedTransferCheck)

        If result = "All checks cleared" Then
            proceed = True
        Else
            MsgBox(result)
            proceed = False
        End If

        If proceed Then
            iweb3 = New Web3(delegatedAccount, "https://rinkeby.infura.io/")
            Dim SignedTransferHandler = iweb3.Eth.GetContractHandler(tokenContractAddress)

            signedTransfer.Gas = Await SignedTransferHandler.EstimateGasAsync(signedTransfer)
            signedTransfer.Gas = signedTransfer.Gas
            Dim transactionReceipt = Await SignedTransferHandler.SendRequestAndWaitForReceiptAsync(signedTransfer)

            MsgBox("Transaction included in block number: " & transactionReceipt.BlockNumber.Value.ToString & ", transaction hash: " & transactionReceipt.TransactionHash.ToString)
        End If

        ' reenable controls
        btnExec.Enabled = True
        txtAmt.Enabled = True
        txtToAddress.Enabled = True
        txtDeployPrivKey.Enabled = True
    End Sub

    Public Function RemovePrefix(ByVal input As String) As String
        If input.Substring(0, 2) = "0x" Then
            input = input.Substring(2)
        End If
        Return input
    End Function

    Private Sub btnExec_Click(sender As Object, e As EventArgs) Handles btnExec.Click
        'execute transfer
        Call testContract()
    End Sub

    Private Sub txtAmt_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmt.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAmt.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        txtAmt.Text = digitsOnly.Replace(txtAmt.Text, "")
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtDeployedAt.Text = "0x1a073cbe88718403c3e521494e1d0d263252ecb3"
        txtHolderAddress.Text = account.Address
    End Sub
End Class
