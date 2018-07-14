Imports Nethereum.Hex.HexTypes
Imports Nethereum.Web3
Imports Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions
Imports Nethereum.Signer.EthereumMessageSigner
Imports Nethereum.Util
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Text.RegularExpressions


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

        ' ABI and bytecode of the deployed contract
        Dim abi = "[{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""distributeTokens"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""isTransferable"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""distributionAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signedTransferSig"",""outputs"":[{""name"":"""",""type"":""bytes4""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""balance"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""tokenOwner"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""tokens"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""},{""name"":""sig"",""type"":""bytes""},{""name"":""feeAccount"",""type"":""address""}],""name"":""signedTransferCheck"",""outputs"":[{""name"":""result"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_value"",""type"":""uint256""}],""name"":""burnSent"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""owner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""getNextNonce"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""from"",""type"":""address""},{""name"":""to"",""type"":""address""},{""name"":""transferAmount"",""type"":""uint256""},{""name"":""fee"",""type"":""uint256""},{""name"":""nonce"",""type"":""uint256""}],""name"":""signedTransferHash"",""outputs"":[{""name"":""hash"",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""enableTransfers"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_setAddress"",""type"":""address""}],""name"":""setDistributionAddress"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""hash"",""type"":""bytes32""},{""name"":""sig"",""type"":""bytes""}],""name"":""ecrecoverFromSig"",""outputs"":[{""name"":""recoveredAddress"",""type"":""address""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""},{""name"":""_spender"",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":""remaining"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""signingPrefix"",""outputs"":[{""name"":"""",""type"":""bytes""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""newOwner"",""type"":""address""}],""name"":""transferOwnership"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""burner"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_from"",""type"":""address""},{""indexed"":true,""name"":""_to"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""_owner"",""type"":""address""},{""indexed"":true,""name"":""_spender"",""type"":""address""},{""indexed"":false,""name"":""_value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]"
        Dim byteCode = "0x60806040526008805460a060020a60ff02191690553480156200002157600080fd5b506040805180820190915260198082527f546573742044656c656761746520546f6b656e2076302e3033000000000000006020909201918252620000689160049162000132565b506005805460ff19166012178155604080518082019091528181527f5444454c540000000000000000000000000000000000000000000000000000006020909101908152620000bb916006919062000132565b5060055460ff16600a0a633b9aca000260078190556003805433600160a060020a03199091168117909155600081815260208181526040808320859055805194855251929391927fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef9281900390910190a3620001d7565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106200017557805160ff1916838001178555620001a5565b82800160010185558215620001a5579182015b82811115620001a557825182559160200191906001019062000188565b50620001b3929150620001b7565b5090565b620001d491905b80821115620001b35760008155600101620001be565b90565b61151680620001e76000396000f30060806040526004361061013d5763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166306fdde038114610142578063095ea7b3146101cc578063158a49881461020457806318160ddd1461022a5780632121dc751461025157806323b872dd14610266578063313ce5671461029057806337fb7e21146102bb5780635b1ea858146102ec57806370a08231146103365780637532eaac146103575780637c0fbc31146103df578063879f30ad146104675780638da5cb5b1461047f57806390193b7c1461049457806395d89b41146104b557806396cfd124146104ca578063a9059cbb146104fa578063af35c6c71461051e578063b89fc89e14610533578063d4acaf6c14610554578063dd62ed3e146105b2578063e2cc7a51146105d9578063f2fde38b146105ee575b600080fd5b34801561014e57600080fd5b5061015761060f565b6040805160208082528351818301528351919283929083019185019080838360005b83811015610191578181015183820152602001610179565b50505050905090810190601f1680156101be5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b3480156101d857600080fd5b506101f0600160a060020a036004351660243561069d565b604080519115158252519081900360200190f35b34801561021057600080fd5b50610228600160a060020a036004351660243561073f565b005b34801561023657600080fd5b5061023f61077c565b60408051918252519081900360200190f35b34801561025d57600080fd5b506101f06107bf565b34801561027257600080fd5b506101f0600160a060020a03600435811690602435166044356107cf565b34801561029c57600080fd5b506102a56107fd565b6040805160ff9092168252519081900360200190f35b3480156102c757600080fd5b506102d0610806565b60408051600160a060020a039092168252519081900360200190f35b3480156102f857600080fd5b50610301610815565b604080517fffffffff000000000000000000000000000000000000000000000000000000009092168252519081900360200190f35b34801561034257600080fd5b5061023f600160a060020a0360043516610839565b34801561036357600080fd5b50604080516020600460a43581810135601f81018490048402850184019095528484526101f0948235600160a060020a039081169560248035909216956044359560643595608435953695929460c494909390920191819084018382808284375094975050509235600160a060020a0316935061085492505050565b3480156103eb57600080fd5b50604080516020600460a43581810135601f8101849004840285018401909552848452610157948235600160a060020a039081169560248035909216956044359560643595608435953695929460c494909390920191819084018382808284375094975050509235600160a060020a0316935061088a92505050565b34801561047357600080fd5b50610228600435610c39565b34801561048b57600080fd5b506102d0610cf6565b3480156104a057600080fd5b5061023f600160a060020a0360043516610d05565b3480156104c157600080fd5b50610157610d20565b3480156104d657600080fd5b5061023f600160a060020a0360043581169060243516604435606435608435610d7b565b34801561050657600080fd5b506101f0600160a060020a0360043516602435610e67565b34801561052a57600080fd5b50610228610e93565b34801561053f57600080fd5b50610228600160a060020a0360043516610ed0565b34801561056057600080fd5b5060408051602060046024803582810135601f81018590048502860185019096528585526102d0958335953695604494919390910191908190840183828082843750949750610f169650505050505050565b3480156105be57600080fd5b5061023f600160a060020a0360043581169060243516610feb565b3480156105e557600080fd5b50610157611016565b3480156105fa57600080fd5b50610228600160a060020a036004351661104d565b6004805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156106955780601f1061066a57610100808354040283529160200191610695565b820191906000526020600020905b81548152906001019060200180831161067857829003601f168201915b505050505081565b60008115806106cd5750336000908152600160209081526040808320600160a060020a0387168452909152902054155b15156106d857600080fd5b336000818152600160209081526040808320600160a060020a03881680855290835292819020869055805186815290519293927f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925929181900390910190a350600192915050565b600854600160a060020a03163314806107625750600354600160a060020a031633145b151561076d57600080fd5b6107778282611093565b505050565b600080805260208190527fad3228b676f7d3cd4284a5443f17f1962b36e491b30a40b2405849e597ba5fb5546007546107ba9163ffffffff61112f16565b905090565b60085460a060020a900460ff1681565b60085460009060a060020a900460ff1615156107ea57600080fd5b6107f5848484611141565b949350505050565b60055460ff1681565b600854600160a060020a031681565b7f7532eaac0000000000000000000000000000000000000000000000000000000081565b600160a060020a031660009081526020819052604090205490565b60085460009060a060020a900460ff16151561086f57600080fd5b61087e888888888888886112a6565b98975050505050505050565b6060600061089b8989898989610d7b565b600160a060020a038a1660009081526002602052604090205490915085146108f85760408051808201909152601581527f4e6f6e636520646f6573206e6f74206d617463682e000000000000000000000060208201529150610c2d565b600160a060020a0389161580610a195750604080518082018252601c8082527f19457468657265756d205369676e6564204d6573736167653a0a33320000000060208084019182529351610a0394869391019182918083835b602083106109705780518252601f199092019160209182019101610951565b51815160209384036101000a600019018019909216911617905292019384525060408051808503815293820190819052835193945092839250908401908083835b602083106109d05780518252601f1990920191602091820191016109b1565b6001836020036101000a038019825116818451168082178552505050505050905001915050604051809103902085610f16565b600160a060020a031689600160a060020a031614155b15610a8257606060405190810160405280603281526020017f4d69736d6174636820696e207369676e696e67206163636f756e74206f72207081526020017f6172616d65746572206d69736d617463682e00000000000000000000000000008152509150610c2d565b600160a060020a038916600090815260208190526040902054871115610b0657606060405190810160405280603181526020017f5472616e7366657220616d6f756e74206578636565647320746f6b656e20626181526020017f6c616e6365206f6e20616464726573732e0000000000000000000000000000008152509150610c2d565b600160a060020a038916600090815260208190526040902054610b2f888863ffffffff6114bb16565b1115610b9957606060405190810160405280602481526020017f496e73756666696369656e7420746f6b656e7320746f2070617920666f72206681526020017f6565732e000000000000000000000000000000000000000000000000000000008152509150610c2d565b600160a060020a0383166000908152602081905260409020548681011015610bf65760408051808201909152600f81527f4f766572666c6f77206572726f722e000000000000000000000000000000000060208201529150610c2d565b60408051808201909152601281527f416c6c20636865636b7320636c65617265640000000000000000000000000000602082015291505b50979650505050505050565b6000808211610c4757600080fd5b33600090815260208190526040902054821115610c6357600080fd5b5033600081815260208190526040902054610c84908363ffffffff61112f16565b600160a060020a038216600090815260208190526040902055600754610cb0908363ffffffff61112f16565b600755604080518381529051600160a060020a038316917fcc16f5dbb4873280815c1ee09dbd06736cffcc184412cf7a71a0fdb75d397ca5919081900360200190a25050565b600354600160a060020a031681565b600160a060020a031660009081526002602052604090205490565b6006805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156106955780601f1061066a57610100808354040283529160200191610695565b604080517f7532eaac000000000000000000000000000000000000000000000000000000006020808301919091526c010000000000000000000000003081026024840152600160a060020a03808a1682026038850152881602604c830152606082018690526080820185905260a08083018590528351808403909101815260c0909201928390528151600093918291908401908083835b60208310610e315780518252601f199092019160209182019101610e12565b5181516020939093036101000a600019018019909116921691909117905260405192018290039091209998505050505050505050565b60085460009060a060020a900460ff161515610e8257600080fd5b610e8c8383611093565b9392505050565b600354600160a060020a03163314610eaa57600080fd5b6008805474ff0000000000000000000000000000000000000000191660a060020a179055565b600354600160a060020a03163314610ee757600080fd5b6008805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60008060008084516041141515610f305760009350610fe2565b50505060208201516040830151606084015160001a601b60ff82161015610f5557601b015b8060ff16601b14158015610f6d57508060ff16601c14155b15610f7b5760009350610fe2565b60408051600080825260208083018085528a905260ff8516838501526060830187905260808301869052925160019360a0808501949193601f19840193928390039091019190865af1158015610fd5573d6000803e3d6000fd5b5050506020604051035193505b50505092915050565b600160a060020a03918216600090815260016020908152604080832093909416825291909152205490565b60408051808201909152601c81527f19457468657265756d205369676e6564204d6573736167653a0a333200000000602082015281565b600354600160a060020a0316331461106457600080fd5b6003805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b336000908152602081905260408120546110b3908363ffffffff61112f16565b3360009081526020819052604080822092909255600160a060020a038516815220546110e5908363ffffffff6114bb16565b600160a060020a038416600081815260208181526040918290209390935580518581529051919233926000805160206114cb8339815191529281900390910190a350600192915050565b60008282111561113b57fe5b50900390565b6000600160a060020a038316151561115857600080fd5b600160a060020a03841660009081526020819052604090205482111561117d57600080fd5b600160a060020a03841660009081526001602090815260408083203384529091529020548211156111ad57600080fd5b600160a060020a0384166000908152602081905260409020546111d6908363ffffffff61112f16565b600160a060020a03808616600090815260208190526040808220939093559085168152205461120b908363ffffffff6114bb16565b600160a060020a0380851660009081526020818152604080832094909455918716815260018252828120338252909152205461124d908363ffffffff61112f16565b600160a060020a03808616600081815260016020908152604080832033845282529182902094909455805186815290519287169391926000805160206114cb833981519152929181900390910190a35060019392505050565b6000806112b68989898989610d7b565b9050600160a060020a0389161580159061132c5750604080518082018252601c8082527f19457468657265756d205369676e6564204d6573736167653a0a333200000000602080840191825293516113179486939101918291808383610970565b600160a060020a031689600160a060020a0316145b151561133757600080fd5b600160a060020a038916600090815260026020526040902054851461135b57600080fd5b600160a060020a038916600090815260026020908152604080832060018901905590829052902054611393908863ffffffff61112f16565b600160a060020a03808b1660009081526020819052604080822093909355908a16815220546113c8908863ffffffff6114bb16565b600160a060020a03808a16600081815260208181526040918290209490945580518b815290519193928d16926000805160206114cb83398151915292918290030190a3600160a060020a038916600090815260208190526040902054611434908763ffffffff61112f16565b600160a060020a03808b166000908152602081905260408082209390935590851681522054611469908763ffffffff6114bb16565b600160a060020a03808516600081815260208181526040918290209490945580518a815290519193928d16926000805160206114cb83398151915292918290030190a350600198975050505050505050565b600082820183811015610e8c57fe00ddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3efa165627a7a72305820a1b3fd1bdcbb85ae7f4412c3e57e2a5e6dc98e9bbed954891a99ab190c1a66e60029"
        Dim iweb3 = New Web3(randomAccount, "https://rinkeby.infura.io/")

        ' SHA3 hash of function
        Dim functionSig = RemovePrefix(Web3.Sha3("signedTransfer(address,address,uint256,uint256,uint256,bytes,address)").Substring(0, 8))

        ' contract address - Get contract
        Dim tokenContractAddress = txtDeployedAt.Text
        Dim tokencontract = iweb3.Eth.GetContract(abi, tokenContractAddress)

        ' address where we are sending tokens to
        Dim toAccount = txtToAddress.Text
        Dim fromAccount = txtHolderAddress.Text

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
        Dim sig = obj.Sign(hash, pKey).HexToByteArray
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