Imports System.Numerics
Imports Nethereum.Contracts
Imports Nethereum.Signer
Imports Nethereum.ABI
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Util
Imports Nethereum.Hex.HexConvertors.Extensions
Imports EthereumUtil

Public Interface IDelegatedSignedFunction
    Property Owner As String
    Property Fee As BigInteger
    Property DelegatedNonce As BigInteger
    Property Sig As Byte()
    Property FeeAccount As String
End Interface


' token implementation of the generic signer service class
Public Class TokenDelegatedSignerService
    Inherits DelegatedSignerService(Of TransferFunction, SignedTransferFunction)

    Public Overrides Function MapDelegatedFunctionToSignedFunction(delegatedFunction As TransferFunction, delegatedSignedFunction As SignedTransferFunction) As SignedTransferFunction
        delegatedSignedFunction.To = delegatedFunction.To
        delegatedSignedFunction.Tokens = delegatedFunction.Value
        Return delegatedSignedFunction
    End Function

    Public Overrides Function MapSignedFunctionToDelegatedSignedFunction(delegatedFunction As TransferFunction, delegatedSignedFunction As SignedTransferFunction) As TransferFunction
        delegatedFunction.To = delegatedSignedFunction.To
        delegatedFunction.Value = delegatedSignedFunction.Tokens
        Return delegatedFunction
    End Function
End Class



' generic signer service class
Public MustInherit Class DelegatedSignerService(Of TDelegatedFunction As {FunctionMessage, New},
                                                   TDelegatedSignedFunction As {FunctionMessage, IDelegatedSignedFunction, New})

    Public Property ContractAddress As String

    ' returns Sha3Signature of the delegated function
    Public Function GetDelegatedFunctionSignature()
        Return ABITypedRegistry.GetFunctionABI(Of TDelegatedSignedFunction).Sha3Signature
    End Function

    Public Function GetHashToSign(delegatedFunction As TDelegatedFunction, ByVal owner As String, ByVal delegatedNonce As BigInteger, ByVal fee As BigInteger) As String
        Dim abiEncode = New ABIEncode
        Return Sha3Keccack.Current.CalculateHashFromHex(GetDelegatedFunctionSignature() & abiEncode.GetABIEncodedPacked(New ABIValue("address", ContractAddress)).ToHex &
                    abiEncode.GetABIEncodedPacked(New ABIValue("address", owner)).ToHex & delegatedFunction.GetParamsEncodedPacked().ToHex &
                    abiEncode.GetABIEncodedPacked(New ABIValue("uint256", fee)).ToHex & abiEncode.GetABIEncodedPacked(New ABIValue("uint256", delegatedNonce)).ToHex)
    End Function

    Public Function GetSignature(delegatedFunction As TDelegatedFunction, ByVal delegatedNonce As BigInteger, ByVal fee As BigInteger, ByVal privateKey As String) As Byte()
        Dim ethEcKey = New EthECKey(privateKey)
        Dim messageSigner = New Nethereum.Signer.EthereumMessageSigner()
        Return messageSigner.Sign(GetHashToSign(delegatedFunction, ethEcKey.GetPublicAddress(), delegatedNonce, fee).HexToByteArray(), ethEcKey).HexToByteArray
    End Function

    Public Function GetSignedRequest(ByRef delegatedSignedFunctionToBeSigned As TDelegatedSignedFunction, ByVal privateKey As String) As TDelegatedSignedFunction
        Dim delegatedFunction = MapSignedFunctionToDelegatedSignedFunction(New TDelegatedFunction(), delegatedSignedFunctionToBeSigned)
        delegatedSignedFunctionToBeSigned.Sig = GetSignature(delegatedFunction, delegatedSignedFunctionToBeSigned.DelegatedNonce, delegatedSignedFunctionToBeSigned.Fee, privateKey)
        Return delegatedSignedFunctionToBeSigned
    End Function

    Public MustOverride Function MapDelegatedFunctionToSignedFunction(delegatedFunction As TDelegatedFunction,
                                                                      delegatedSignedFunction As TDelegatedSignedFunction) As TDelegatedSignedFunction

    Public MustOverride Function MapSignedFunctionToDelegatedSignedFunction(delegatedFunction As TDelegatedFunction,
                                                                            delegatedSignedFunction As TDelegatedSignedFunction) As TDelegatedFunction

End Class


Partial Public Class SignedTransferFunction
    Inherits SignedTransferFunctionBase
    Implements IDelegatedSignedFunction

    Private pTokenOwner As String
    Private pDelegatedNonce As BigInteger
    Private pFee As BigInteger
    Private pSig As Byte()
    Private pFeeAccount As String

    Public Overrides Property TokenOwner As String Implements IDelegatedSignedFunction.Owner
        Get
            Return pTokenOwner
        End Get
        Set(value As String)
            pTokenOwner = value
        End Set
    End Property
    Public Overrides Property intNonce As BigInteger Implements IDelegatedSignedFunction.DelegatedNonce
        Get
            Return pDelegatedNonce
        End Get
        Set(value As BigInteger)
            pDelegatedNonce = value
        End Set
    End Property
    Public Overrides Property Fee As BigInteger Implements IDelegatedSignedFunction.Fee
        Get
            Return pFee
        End Get
        Set(value As BigInteger)
            pFee = value
        End Set
    End Property
    Public Overrides Property Sig As Byte() Implements IDelegatedSignedFunction.Sig
        Get
            Return pSig
        End Get
        Set(value As Byte())
            pSig = value
        End Set
    End Property
    Public Overrides Property FeeAccount As String Implements IDelegatedSignedFunction.FeeAccount
        Get
            Return pFeeAccount
        End Get
        Set(value As String)
            pFeeAccount = value
        End Set
    End Property
End Class

Partial Public Class SignedTransferCheckFunction
    Inherits SignedTransferCheckFunctionBase
End Class

Partial Public Class SignedTransferFunction
    Inherits SignedTransferFunctionBase
End Class

Partial Public Class TransferFunction
    Inherits TransferFunctionBase
End Class


' contract function implementation

<[Function]("transfer", "bool")>
Public Class TransferFunctionBase
    Inherits FunctionMessage

    <[Parameter]("address", "_to", 1)>
    Public Overridable Property [To] As String
    <[Parameter]("uint256", "_value", 2)>
    Public Overridable Property [Value] As BigInteger
End Class

<[Function]("signedTransferCheck", "string")>
Public Class SignedTransferCheckFunctionBase
    Inherits FunctionMessage

    <[Parameter]("address", "from", 1)>
    Public Overridable Property [From] As String
    <[Parameter]("address", "to", 2)>
    Public Overridable Property [To] As String
    <[Parameter]("uint256", "transferAmount", 3)>
    Public Overridable Property [TransferAmount] As BigInteger
    <[Parameter]("uint256", "fee", 4)>
    Public Overridable Property [Fee] As BigInteger
    <[Parameter]("uint256", "nonce", 5)>
    Public Overridable Property [intNonce] As BigInteger
    <[Parameter]("bytes", "sig", 6)>
    Public Overridable Property [Sig] As Byte()
    <[Parameter]("address", "feeAccount", 7)>
    Public Overridable Property [FeeAccount] As String
End Class

<[Function]("signedTransferHash", "bytes32")>
Public Class SignedTransferHashFunctionBase
    Inherits FunctionMessage

    <[Parameter]("address", "from", 1)>
    Public Overridable Property [From] As String
    <[Parameter]("address", "to", 2)>
    Public Overridable Property [To] As String
    <[Parameter]("uint256", "transferAmount", 3)>
    Public Overridable Property [TransferAmount] As BigInteger
    <[Parameter]("uint256", "fee", 4)>
    Public Overridable Property [Fee] As BigInteger
    <[Parameter]("uint256", "nonce", 5)>
    Public Overridable Property [intNonce] As BigInteger

End Class

<[Function]("signedTransfer", "bool")>
Public Class SignedTransferFunctionBase
    Inherits FunctionMessage

    <[Parameter]("address", "tokenOwner", 1)>
    Public Overridable Property [TokenOwner] As String
    <[Parameter]("address", "to", 2)>
    Public Overridable Property [To] As String
    <[Parameter]("uint256", "tokens", 3)>
    Public Overridable Property [Tokens] As BigInteger
    <[Parameter]("uint256", "fee", 4)>
    Public Overridable Property [Fee] As BigInteger
    <[Parameter]("uint256", "nonce", 5)>
    Public Overridable Property [intNonce] As BigInteger
    <[Parameter]("bytes", "sig", 6)>
    Public Overridable Property [Sig] As Byte()
    <[Parameter]("address", "feeAccount", 7)>
    Public Overridable Property [FeeAccount] As String
End Class

<[Function]("getNextNonce", "uint256")>
Public Class GetNextNonceFunction
    Inherits FunctionMessage

    <[Parameter]("address", "_owner", 1)>
    Public Overridable Property [Owner] As String

End Class