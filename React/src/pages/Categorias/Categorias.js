import React, { Component } from 'react';
import Rodape from '../../components/Rodape/Rodape';
import { MDBContainer, MDBBtn, MDBModal, MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput } from 'mdbreact';
import Cabecalho from '../../components/Cabecalho/Cabecalho';

// Importante
// 1. Damos o bind quando não usamos arrow function 
// 2. bind - Ligação, Vínculo
// 3. console log - Debugar - Retornos
// 4. preventDefault - Impede a página de ser carregada
// 5. Toggle - Abrir e fechar modal - Função usando o MMDB
// 6. Fetch - Onde ele vai buscar no bando de dados - Endpoint

class Categorias extends Component {
// Base usada para criar os states (Constructor)
    constructor(){
        // Super - usado para manipular os states que são herdados de Component
        super();
        this.state = {
            lista : [],
            nome : "",
            loading: false,
            erroMsg : "",
            modal: false,
            // Usamos para armazenar os dados a serem alterados
            editarModal : {
                categoriaId: "",
                titulo: ""
            }          
        }
    }

    toggle = () => {
        this.setState({
          modal: !this.state.modal
        });
    }

// Método GET 
    listaAtualizada = () =>{

        this.setState({ loading : true});

        fetch("http://localhost:5000/api/categoria")
            .then(response => response.json())
            .then(data => {
                this.setState( {lista: data } )
                this.setState({ loading : false});
            })
            .catch(error => {
                this.setState({ loading : false});
                console.log(error);
            })
    }

// Método POST
    cadastrarCategoria(event){

        event.preventDefault();
        console.log("Cadastrando");

        fetch("http://localhost:5000/api/categoria", {
           method : "POST",
           body: JSON.stringify({ titulo : this.state.nome }),
           headers : { 
               "Content-Type" : "application/json"
           }
        })
        .then(response => response.json())
        .then(response => {
            console.log(response);
            this.listaAtualizada();
        })
        .catch(error => console.log(error))
    }

    // Método PUT - Mais demorado 
    // Adcionado quando clicamos no botão editar para capturar e salvar no state os dados atuais
    
    alterarCategoria = (produto) => {
        console.log(produto);

        this.setState({
            editarModal: {
                categoriaId : produto.categoriaId,
                titulo: produto.titulo
            }
        })

        // Abrir Modal
        this.toggle();
    }
    // Método que salve efetivamente as alterações 

    salvarAlteracoes = (event) => {

        event.preventDefault();
        console.log(this.state.editarModal);

        fetch("http://localhost:5000/api/categoria/"+this.state.editarModal.categoriaId, {
           method : "PUT",
           body: JSON.stringify(this.state.editarModal),
           headers : { 
               "Content-Type" : "application/json"
           }
        })
        .then(response => response.json())
        .then(
            // Atrasado na requisição
            setTimeout(() => {
                this.listaAtualizada()
            }, 1000)
        )
        .catch(error => console.log(error))        

        // FecharModal
        this.toggle();
    }

    // atualiza o valor do input
    atualizaNome(input){
        this.setState({ nome : input.target.value })
    }
    
    // Atualiza os states dos inputs dentro do modal
    atualizaEditarModalTitulo(input){
        this.setState({ 
            editarModal: {
                categoriaId : this.state.editarModal.categoriaId,
                titulo: input.target.value
            }
        })
    }

    // Método DELETE
    deletarCategoria = (id) =>{
        
        this.setState({ erroMsg : "" })
        
        console.log("Excluindo");
        
        fetch("http://localhost:5000/api/categoria/"+id, {
           method : "DELETE",
           headers : { 
               "Content-Type" : "application/json"
           }
        })
        .then(response => response.json())
        .then(response => {
            console.log(response);
            this.listaAtualizada();
            this.setState( () => ({ lista: this.state.lista }));
        })
        .catch(error => {
            console.log(error)
            this.setState({ erroMsg : "Não foi possível excluir, verifique se não há eventos cadastrados nesta Categoria" })
        })
    }

    

    // Ciclo de vida dos componentes 

    UNSAFE_componentWillMount(){
        document.title = this.props.titulo_pagina;
        //console.log('Will');
    }

    componentDidMount(){
        //console.log('Did');
        this.listaAtualizada();
    }

    UNSAFE_componentWillUpdate(){
        //console.log("WillUpdate");
    }

    componentDidUpdate(){
        //console.log("Update");
    }

    componentWillUnmount(){
        //console.log("Unmount")
    }

    render(){

        let {loading} = this.state;

        return(
            
            <div className="App">
                <Cabecalho/>

                <main className="conteudoPrincipal">
                    <section className="conteudoPrincipal-cadastro">
                    <h1 className="conteudoPrincipal-cadastro-titulo">Categorias</h1>
                    
                    <div className="container" id="conteudoPrincipal-lista">
                        <table id="tabela-lista">
                        <thead>
                            <tr>
                            <th>#</th>
                            <th>Título</th>
                            <th>Ação</th>
                            </tr>
                        </thead>

                        <tbody id="tabela-lista-corpo">
                            {
                                // Mapa que vai retornar a lista e coloca-se uma chave
                                // porque cada linha em JSX precisa de um ID único
                                this.state.lista.map(function(categoria){
                                    return (
                                        <tr key={categoria.categoriaId}>
                                            <td>{categoria.categoriaId}</td>
                                            <td>{categoria.titulo}</td>
                                            <td>
                                                <button onClick={e => this.alterarCategoria(categoria)}>Alterar</button>
                                                <button onClick={e => this.deletarCategoria(categoria.categoriaId)}>Excluir</button>
                                            </td>
                                        </tr>
                                    );
                                }.bind(this))
                            }
                        </tbody>
                        </table>
                    </div>

                    {loading && <i className="fa fa-spin fa-spinner fa-2x"></i>}
                    {this.state.erroMsg && <div className="text-danger">{this.state.erroMsg}</div>}

                    <div className="container" id="conteudoPrincipal-cadastro">
                        <h2 className="conteudoPrincipal-cadastro-titulo">
                        Cadastrar Categoria
                        </h2>
                        <form onSubmit={this.cadastrarCategoria}>
                        <div className="container">

                            <input
                                type="text"
                                className="class__categoria"
                                id="input__categoria"
                                placeholder="tipo do evento"
                                value={this.state.nome}
                                onChange={this.atualizaNome.bind(this)}
                            />

                            <button
                            id="btn__cadastrar"
                            className="conteudoPrincipal-btn conteudoPrincipal-btn-cadastro"
                            >
                            Cadastrar
                            </button>
                        </div>
                        </form>
                    </div>
                    </section>
                </main>
    {/* Modal Material Design Bootstrap - Utilizamos para poder alterar o input dando setState */}
                <MDBContainer>
                    {/* <MDBBtn onClick={this.toggle}>Modal</MDBBtn> */}
                    <form onSubmit={this.salvarAlteracoes}>
                        <MDBModal isOpen={this.state.modal} toggle={this.toggle}>
                            <MDBModalHeader toggle={this.toggle}>Editar <b>{this.state.editarModal.titulo}</b> </MDBModalHeader>
                        <MDBModalBody>
                            <MDBInput 
                                label="Categoria" 
                                value={this.state.editarModal.titulo}
                                onChange={this.atualizaEditarModalTitulo.bind(this)} 
                            />
                        </MDBModalBody>
                        <MDBModalFooter>
                            <MDBBtn color="secondary" onClick={this.toggle}>Fechar</MDBBtn>
                            <MDBBtn color="primary" type="submit">Alterar</MDBBtn>
                        </MDBModalFooter>
                        </MDBModal>
                    </form>
                </MDBContainer>

                <Rodape />
            </div>
        );
    }
}

export default Categorias;