Imports Nethereum.Hex.HexTypes
Imports Nethereum.Web3
Imports Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions
Imports Nethereum.Signer.EthereumMessageSigner
Imports Nethereum.Util
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Text.RegularExpressions


Public Class MainForm
    Private Async Sub testContract()
        Dim SHK As New Sha3Keccack

        ' from account - account we are transferring from
        Dim privateKey As New Nethereum.Signer.EthECKey(txtPrivKey.Text)
        Dim account = New Nethereum.Web3.Accounts.Account(privateKey)

        ' this can be a random account - We just need this to initialise the contract object and read the nonce
        Dim anEntirelyRandomAccount As New Nethereum.Signer.EthECKey("0000000000000000000000000000000000000000000000000000000000000001")
        Dim randomAccount = New Nethereum.Web3.Accounts.Account(anEntirelyRandomAccount)

        ' ABI and bytecode of the deployed contract
        Dim abi = "[{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""distributeTokens"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""isTransferable"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""distributionAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signedTransferSig"",""outputs"":[{""name"":"""",""type"":""bytes4""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""balance"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""tokenOwner"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""tokens"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransferCheck"",""outputs"":[{""name"":""result"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_value"",""type"":""uint256""}],""name"":""burnSent"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""owner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""getNextNonce"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""}],""name"":""signedTransferHash"",""outputs"":[{""name"":""hash"",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""enableTransfers"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_setAddress"",""type"":""address""}],""name"":""setDistributionAddress"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""hash"",""type"":""bytes32""},{""name"":""sig"",""type"":""bytes""}],""name"":""ecrecoverFromSig"",""outputs"":[{""name"":""recoveredAddress"",""type"":""address""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""},{""name"":""_spender"",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":""remaining"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signingPrefix"",""outputs"":[{""name"":"""",""type"":""bytes""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""newOwner"",""type"":""address""}],""name"":""transferOwnership"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""burner"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_from"",""type"":""address""},{""indexed"":true,""name"":""_to"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_owner"",""type"":""address""},{""indexed"":true,""name"":""_spender"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]"
        Dim byteCode = "0x60606040526008805460a060020a60ff021916905534156200002057600080fd5b60408051908101604052601981527f546573742044656c656761746520546f6b656e2076302e303200000000000000602082015260049080516200006992916020019062000141565b506005805460ff1916601217905560408051908101604052600381527f544454000000000000000000000000000000000000000000000000000000000060208201526006908051620000c092916020019062000141565b5060055460ff16600a0a633b9aca0002600781905560038054600160a060020a03191633600160a060020a0316908117909155600081815260208190526040808220849055919290917fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef91905190815260200160405180910390a3620001e6565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106200018457805160ff1916838001178555620001b4565b82800160010185558215620001b4579182015b82811115620001b457825182559160200191906001019062000197565b50620001c2929150620001c6565b5090565b620001e391905b80821115620001c25760008155600101620001cd565b90565b6114ea80620001f66000396000f30060606040526004361061013d5763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166306fdde038114610142578063095ea7b3146101cc578063158a49881461020257806318160ddd146102265780632121dc751461024b57806323b872dd1461025e578063313ce5671461028657806337fb7e21146102af5780635b1ea858146102de57806370a08231146103235780637532eaac146103425780637c0fbc31146103c1578063879f30ad146104405780638da5cb5b1461045657806390193b7c1461046957806395d89b411461048857806396cfd1241461049b578063a9059cbb146104c9578063af35c6c7146104eb578063b89fc89e146104fe578063d4acaf6c1461051d578063dd62ed3e14610573578063e2cc7a5114610598578063f2fde38b146105ab575b600080fd5b341561014d57600080fd5b6101556105ca565b60405160208082528190810183818151815260200191508051906020019080838360005b83811015610191578082015183820152602001610179565b50505050905090810190601f1680156101be5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101d757600080fd5b6101ee600160a060020a0360043516602435610668565b604051901515815260200160405180910390f35b341561020d57600080fd5b610224600160a060020a036004351660243561070e565b005b341561023157600080fd5b610239610753565b60405190815260200160405180910390f35b341561025657600080fd5b6101ee610796565b341561026957600080fd5b6101ee600160a060020a03600435811690602435166044356107a6565b341561029157600080fd5b6102996107d4565b60405160ff909116815260200160405180910390f35b34156102ba57600080fd5b6102c26107dd565b604051600160a060020a03909116815260200160405180910390f35b34156102e957600080fd5b6102f16107ec565b6040517bffffffffffffffffffffffffffffffffffffffffffffffffffffffff19909116815260200160405180910390f35b341561032e57600080fd5b610239600160a060020a0360043516610810565b341561034d57600080fd5b6101ee600160a060020a0360048035821691602480359091169160443591606435916084359160c49060a43590810190830135806020601f8201819004810201604051908101604052818152929190602084018383808284375094965050509235600160a060020a0316925061082b915050565b34156103cc57600080fd5b610155600160a060020a0360048035821691602480359091169160443591606435916084359160c49060a43590810190830135806020601f8201819004810201604051908101604052818152929190602084018383808284375094965050509235600160a060020a03169250610861915050565b341561044b57600080fd5b610224600435610bbc565b341561046157600080fd5b6102c2610c84565b341561047457600080fd5b610239600160a060020a0360043516610c93565b341561049357600080fd5b610155610cae565b34156104a657600080fd5b610239600160a060020a0360043581169060243516604435606435608435610d19565b34156104d457600080fd5b6101ee600160a060020a0360043516602435610dce565b34156104f657600080fd5b610224610dfa565b341561050957600080fd5b610224600160a060020a0360043516610e3b565b341561052857600080fd5b6102c2600480359060446024803590810190830135806020601f82018190048102016040519081016040528181529291906020840183838082843750949650610e8595505050505050565b341561057e57600080fd5b610239600160a060020a0360043581169060243516610f58565b34156105a357600080fd5b610155610f83565b34156105b657600080fd5b610224600160a060020a0360043516610fba565b60048054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156106605780601f1061063557610100808354040283529160200191610660565b820191906000526020600020905b81548152906001019060200180831161064357829003601f168201915b505050505081565b600081158061069a5750600160a060020a03338116600090815260016020908152604080832093871683529290522054155b15156106a557600080fd5b600160a060020a03338116600081815260016020908152604080832094881680845294909152908190208590557f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b9259085905190815260200160405180910390a350600192915050565b60085433600160a060020a0390811691161480610739575060035433600160a060020a039081169116145b151561074457600080fd5b61074e8282611004565b505050565b600080805260208190527fad3228b676f7d3cd4284a5443f17f1962b36e491b30a40b2405849e597ba5fb5546007546107919163ffffffff6110c816565b905090565b60085460a060020a900460ff1681565b60085460009060a060020a900460ff1615156107c157600080fd5b6107cc8484846110da565b949350505050565b60055460ff1681565b600854600160a060020a031681565b7f7532eaac0000000000000000000000000000000000000000000000000000000081565b600160a060020a031660009081526020819052604090205490565b60085460009060a060020a900460ff16151561084657600080fd5b61085588888888888888611248565b98975050505050505050565b61086961148c565b60006108788989898989610d19565b600160a060020a038a1660009081526002602052604090205490915085146108d55760408051908101604052601581527f4e6f6e636520646f6573206e6f74206d617463682e000000000000000000000060208201529150610bb0565b600160a060020a038916158061099c57506109866040805190810160405280601c81526020017f19457468657265756d205369676e6564204d6573736167653a0a333200000000815250826040518083805190602001908083835b6020831061094f5780518252601f199092019160209182019101610930565b6001836020036101000a03801982511681845116179092525050509190910192835250506020019050604051809103902085610e85565b600160a060020a031689600160a060020a031614155b15610a0557606060405190810160405280603281526020017f4d69736d6174636820696e207369676e696e67206163636f756e74206f72207081526020017f6172616d65746572206d69736d617463682e00000000000000000000000000008152509150610bb0565b600160a060020a038916600090815260208190526040902054871115610a8957606060405190810160405280603181526020017f5472616e7366657220616d6f756e74206578636565647320746f6b656e20626181526020017f6c616e6365206f6e20616464726573732e0000000000000000000000000000008152509150610bb0565b600160a060020a038916600090815260208190526040902054610ab2888863ffffffff61147d16565b1115610b1c57606060405190810160405280602481526020017f496e73756666696369656e7420746f6b656e7320746f2070617920666f72206681526020017f6565732e000000000000000000000000000000000000000000000000000000008152509150610bb0565b600160a060020a0383166000908152602081905260409020548681011015610b795760408051908101604052600f81527f4f766572666c6f77206572726f722e000000000000000000000000000000000060208201529150610bb0565b60408051908101604052601281527f416c6c20636865636b7320636c65617265640000000000000000000000000000602082015291505b50979650505050505050565b6000808211610bca57600080fd5b600160a060020a033316600090815260208190526040902054821115610bef57600080fd5b5033600160a060020a038116600090815260208190526040902054610c1490836110c8565b600160a060020a038216600090815260208190526040902055600754610c40908363ffffffff6110c816565b600755600160a060020a0381167fcc16f5dbb4873280815c1ee09dbd06736cffcc184412cf7a71a0fdb75d397ca58360405190815260200160405180910390a25050565b600354600160a060020a031681565b600160a060020a031660009081526002602052604090205490565b60068054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156106605780601f1061063557610100808354040283529160200191610660565b60007f7532eaac000000000000000000000000000000000000000000000000000000003087878787876040517bffffffffffffffffffffffffffffffffffffffffffffffffffffffff1990971687526c01000000000000000000000000600160a060020a03968716810260048901529486168502601888015292909416909202602c8501526040808501929092526060840192909252608083019190915260a090910190518091039020905095945050505050565b60085460009060a060020a900460ff161515610de957600080fd5b610df38383611004565b9392505050565b60035433600160a060020a03908116911614610e1557600080fd5b6008805474ff0000000000000000000000000000000000000000191660a060020a179055565b60035433600160a060020a03908116911614610e5657600080fd5b6008805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b6000806000808451604114610e9d5760009350610f4f565b6020850151925060408501519150606085015160001a9050601b8160ff161015610ec557601b015b8060ff16601b14158015610edd57508060ff16601c14155b15610eeb5760009350610f4f565b6001868285856040516000815260200160405260405193845260ff9092166020808501919091526040808501929092526060840192909252608090920191516020810390808403906000865af11515610f4357600080fd5b50506020604051035193505b50505092915050565b600160a060020a03918216600090815260016020908152604080832093909416825291909152205490565b60408051908101604052601c81527f19457468657265756d205369676e6564204d6573736167653a0a333200000000602082015281565b60035433600160a060020a03908116911614610fd557600080fd5b6003805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b600160a060020a03331660009081526020819052604081205461102d908363ffffffff6110c816565b600160a060020a033381166000908152602081905260408082209390935590851681522054611062908363ffffffff61147d16565b60008085600160a060020a0316600160a060020a031681526020019081526020016000208190555082600160a060020a031633600160a060020a031660008051602061149f8339815191528460405190815260200160405180910390a350600192915050565b6000828211156110d457fe5b50900390565b6000600160a060020a03831615156110f157600080fd5b600160a060020a03841660009081526020819052604090205482111561111657600080fd5b600160a060020a038085166000908152600160209081526040808320339094168352929052205482111561114957600080fd5b600160a060020a038416600090815260208190526040902054611172908363ffffffff6110c816565b600160a060020a0380861660009081526020819052604080822093909355908516815220546111a7908363ffffffff61147d16565b600160a060020a03808516600090815260208181526040808320949094558783168252600181528382203390931682529190915220546111ed908363ffffffff6110c816565b600160a060020a038086166000818152600160209081526040808320338616845290915290819020939093559085169160008051602061149f8339815191529085905190815260200160405180910390a35060019392505050565b6000806112588989898989610d19565b9050600160a060020a038916158015906112ea57506112d56040805190810160405280601c81526020017f19457468657265756d205369676e6564204d6573736167653a0a333200000000815250826040518083805190602001908083836020831061094f5780518252601f199092019160209182019101610930565b600160a060020a031689600160a060020a0316145b15156112f557600080fd5b600160a060020a038916600090815260026020526040902054851461131957600080fd5b600160a060020a038916600090815260026020908152604080832060018901905590829052902054611351908863ffffffff6110c816565b600160a060020a03808b1660009081526020819052604080822093909355908a1681522054611386908863ffffffff61147d16565b600160a060020a03808a16600081815260208190526040908190209390935591908b169060008051602061149f833981519152908a905190815260200160405180910390a3600160a060020a0389166000908152602081905260409020546113f4908763ffffffff6110c816565b600160a060020a03808b166000908152602081905260408082209390935590851681522054611429908763ffffffff61147d16565b600160a060020a03808516600081815260208190526040908190209390935591908b169060008051602061149f8339815191529089905190815260200160405180910390a350600198975050505050505050565b600082820183811015610df357fe5b602060405190810160405260008152905600ddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3efa165627a7a72305820854ea67acab3fbe3db8f5621a8f081461b80723eb78a2f51deb09716782e11970029"
        Dim iweb3 = New Web3(randomAccount, "https://rinkeby.infura.io/")

        ' SHA3 hash of function
        Dim functionSig = RemovePrefix(Web3.Sha3("signedTransfer(address,address,uint256,uint256,uint256,bytes,address)").Substring(0, 8))

        ' contract address - Get contract
        Dim tokenContractAddress = "0x006A9f55eC7B134C59E9cBa6f5fD41cdeC1991f9"
        Dim tokencontract = iweb3.Eth.GetContract(abi, tokenContractAddress)

        ' address where we are sending tokens to
        Dim toAccount = txtToAddress.Text
        Dim fromAccount = account.Address

        Dim tmpAmt As Int32 = Convert.ToInt32(txtAmt.Text)
        Dim tmpAmtFees As Int32 = tmpAmt * 0.01 'we take 1 % fees

        ' sending tokens
        Dim numTokens = RemovePrefix(Web3.Convert.ToWei(tmpAmt).ToString("X16"))
        Dim feeTokens = RemovePrefix(Web3.Convert.ToWei(tmpAmtFees).ToString("X16"))


        ' get nonce
        Dim getNextNounce = tokencontract.GetFunction("getNextNonce")
        Dim noncetmp = Await getNextNounce.CallAsync(Of Integer)(fromAccount)
        Dim nonce = noncetmp.ToString("X16")

        ' close iweb3, we will use this later
        iweb3 = Nothing

        ' generate offchain hash
        Dim offchainHash = SHK.CalculateHashFromHex(functionSig.ToString, tokenContractAddress.PadLeft(40, "0"), fromAccount.PadLeft(40, "0"), toAccount.PadLeft(40, "0"),
                                               numTokens.ToString.PadLeft(64, "0"), feeTokens.ToString.PadLeft(64, "0"), nonce.ToString.PadLeft(64, "0"))

        ' sign the transaction with from account private key
        Dim hash = offchainHash.HexToByteArray
        Dim obj As New Nethereum.Signer.EthereumMessageSigner
        Dim sig = obj.Sign(hash, privateKey).HexToByteArray
        'Dim test = obj.EcRecover(hash, obj.Sign(hash, privateKey.GetPrivateKey))

        ' generate onchain hash
        Dim signedTransferHash = tokencontract.GetFunction("signedTransferHash")
        Dim onchainHash = (Await signedTransferHash.CallAsync(Of Byte())(fromAccount, toAccount, numTokens, feeTokens, nonce)).ToHex

        ' initialise delegate account
        Dim delegatedprivateKey As New Nethereum.Signer.EthECKey(txtDeployPrivKey.Text)
        Dim delegatedAccount = New Nethereum.Web3.Accounts.Account(delegatedprivateKey)
        Dim delegateAddress = delegatedAccount.Address
        Dim feeAccount = delegatedAccount.Address

        ' start checks
        Dim proceed As Boolean
        Dim signedTransferCheck = tokencontract.GetFunction("signedTransferCheck")
        Dim result = Await signedTransferCheck.CallAsync(Of String)(fromAccount, toAccount, numTokens, feeTokens, nonce, sig, feeAccount)

        If result = "All checks cleared" Then
            proceed = True
        Else
            MsgBox(result)
            proceed = False
        End If

        ' compare and ensure they're the same
        If proceed Then
            Dim gas As New HexBigInteger(200000)
            Dim value As New HexBigInteger(0)

            iweb3 = New Web3(delegatedAccount, "https://rinkeby.infura.io/")
            Dim tokencontractDeployer = iweb3.Eth.GetContract(abi, tokenContractAddress)

            Dim signedTransfer = tokencontractDeployer.GetFunction("signedTransfer")
            Dim transactionHash = Await signedTransfer.SendTransactionAndWaitForReceiptAsync(delegateAddress, gas, value, , fromAccount, toAccount, numTokens, feeTokens, nonce, sig, feeAccount)

            MsgBox("Transaction included in block number: " & transactionHash.BlockNumber.Value.ToString & ", transaction hash: " & transactionHash.TransactionHash.ToString)
        End If
    End Sub

    Public Function RemovePrefix(ByVal input As String) As String
        If input.Substring(0, 2) = "0x" Then
            input = input.Substring(2)
        End If
        Return input
    End Function

    Private Sub btnExec_Click(sender As Object, e As EventArgs) Handles btnExec.Click
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
End Class