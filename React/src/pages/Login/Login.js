import React, {Component} from 'react'
// import Rodape from '../../components/Rodape/Rodape';
import Cabecalho from '../../components/Cabecalho/Cabecalho';
import '../../assets/css/login.css'
import axios from 'axios' // Importando o axios

export default class Login extends Component {
    constructor()
    {
        super();
        this.state = {
            email:"",
            senha:""
        }
    }
    // Atualizada estado genérico, para que seja feito uma só vez.
    atualizaEstado = (event) => {
        this.setState({[event.target.name] : event.target.value});
    } 

    realizarLogin = (event) => {
        event.preventDefault();

        let config = {
            headers: {
                "Content-Type":"application/json",
                "Acess-Control-Allow-Origin":"*" // Cors
            }
        }
        axios.post("http://localhost:5000/api/login", {
            email : this.state.email,
            senha : this.state.senha
        }, config)
        .then(response => {
            console.log("Retorno do login: ", response)
        })
        .catch(erro => {
            console.log("Erro: ", erro)
        })
    }
    render() {
        return (
        <div><Cabecalho/> 
            <section class="container flex">      
      <div class="img__login"><div class="img__overlay"></div></div>

      <div class="item__login">
        <div class="row">
          <div class="item">
          </div>
          <div class="item" id="item__title">
            <p class="text__login" id="item__description">
              Bem-vindo! Faça login para acessar sua conta.
            </p>
          </div>
          <form onSubmit={this.realizarLogin}>
            <div class="item"> 
              <input
                class="input__login"
                placeholder="username"
                type="text"
                name="email"  // Deve ser igual ao nome da variável no state para que o atualizaEstado funcione
                value={this.state.email}
                onChange={this.atualizaEstado}
                id="login__email"
              />
            </div>
            <div class="item">
              <input
                class="input__login"
                placeholder="password"
                type="password"
                name="senha"
                value={this.state.senha}
                onChange={this.atualizaEstado}
                id="login__password"
              />
            </div>
            <div class="item">
              <button type="submit" class="btn btn__login" id="btn__login">
                Login
              </button>
            </div>
          </form>
        </div>
      </div>
    </section>
    </div>   
        )
    }
}