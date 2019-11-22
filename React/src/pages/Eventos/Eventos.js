import React, { Component } from 'react';
import Rodape from '../../components/Rodape/Rodape';
import { MDBContainer, MDBBtn, MDBModal, MDBModalBody, MDBModalHeader, MDBModalFooter, MDBInput } from 'mdbreact';
import Cabecalho from '../../components/Cabecalho/Cabecalho';

// Começando a página criando meu construtor 
class Eventos extends Component {
    constructor() {
        super();
        this.state = {
            listaEventos: [],
            listaCategorias: [],
            tituloEvento: "",
            dataEvento: "",
            acessoLivre: false,
            localiacao: "",
            loading: false,
            erroMsg: "",
            editarModal : {
                categoriaId: "",
                tituloEvento: "",
                dataEvento: "",
                acessoLivre: "",
                localizacao: "",

        }
    }

    }

    // Ciclo de vida dos componentes 

    UNSAFE_componentWillMount() {
        //prop para guardar as informações
        console.log("Carregando");
    }

    componentDidMount() {
        //console.log('Did');
        document.title = this.props.titulo_pagina;
        console.log(this.state.listaEventos);
        this.listarEventos();
    }

    UNSAFE_componentWillUpdate() {
        //console.log("WillUpdate");
    }

    componentDidUpdate() {
        //console.log("Update");
        console.log("Atualizando")
    }

    componentWillUnmount() {
        //console.log("Unmount")
        console.log("Saindo")
    }


    // Atualizar o estado de todos os states pra não precisar atualizar um por um


    atualizaEstado = (input) => {
        let nomePropriedade = input.target.name;
        this.setState({
            [input.target.name]: input.target.value
        },
            () => console.log("Novo valor: ", this.state[nomePropriedade])
        );
    }

    // Get - Listagem dos eventos, categorias e localização

    listaEventos = () => {
        fetch("http://localhost:5000/api/Evento")
            .then(response => response.json())
            .then(data => this.setState({ listaEventos: data }))
    }

    listaCategorias = () => {
        fetch("http://localhost:5000/api/Categorias")
            .then(response => response.json())
            .then(data => this.setState({ listaEventos: data }))
    }

    // Post - Cadastrar evento

    cadastrarEvento(event) {

        event.preventDefault();
        console.log("Cadastrando");

        fetch("http://localhost:5000/api/evento", {
            method: "POST",
            body: JSON.stringify({
                titulo: this.state.titulo,
                categoria: (this.state.categoria === undefined) ? '' : this.evento.categoria.titulo,
                acessoLivre: (this.state.acessoLivre === false) ? 'não' : 'sim',
                data: this.state.data,
                localizacao: (this.state.localizacao === undefined) ? '' : this.evento.localizacao.endereco,
            }),

            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(response => {
                console.log(response);
                this.listaEventos();
            })
            .catch(error => console.log(error))
    }

    // PUT - alterar evento

    alterarEvento = (produto) => {
        console.log(produto);

        this.setState({
            editarModal: {
                eventoId: produto.eventoId,
                titulo: produto.titulo,
                categoria: produto.categoria,
                acessoLivre: produto.acessoLivre,
                data: produto.dataEvento,
                localizacao: produto.localizacao

            }
        })

        // Abrir Modal
        this.toggle();
    }

    salvarAlteracoes = (evento) => {

        evento.preventDefault();
        console.log(this.state.editarModal);

        fetch("http://localhost:5000/api/evento/" + this.state.editarModal.eventoId, {
            method: "PUT",
            body: JSON.stringify(this.state.editarModal),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(
                setTimeout(() => {
                    this.listaAtualizada()
                }, 2300)
            )
            .catch(error => console.log(error))

        // FecharModal
        this.toggle();
    }

    toggle = () => {


    }

    // DELETE - deletar evento 
    deletarEvento = (id) => {

        this.setState({ erroMsg: "" })

        console.log("Excluindo");

        fetch("http://localhost:5000/api/evento/" + id, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(response => {
                console.log(response);
                this.listaAtualizada();
                this.setState(() => ({ lista: this.state.lista }));
            })
            .catch(error => {
                console.log(error)
                this.setState({ erroMsg: "Não foi possível excluir, verifique se não há eventos cadastrados nesta Evento" })
            })
    }




    render() {
        let { loading } = this.state;
        return (
            <div className="App">
                <Cabecalho/>
                

                <main className="conteudoPrincipal">
                    <section className="conteudoPrincipal-cadastro">
                        <h1 className="conteudoPrincipal-cadastro-titulo">Eventos</h1>

                        <div className="container" id="conteudoPrincipal-lista">
                            <table id="tabela-lista">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Título</th>
                                        <th>Categoria</th>
                                        <th>AcessoLivre</th>
                                        <th>Data</th>
                                        <th>Localização</th>
                                        <th>Ação</th>
                                    </tr>
                                </thead>

                                <tbody id="tabela-lista-corpo">
                                    {
                                        this.state.listaEventos.map(function (event) {
                                            return (
                                                <tr key={event.eventoId}>
                                                    <td>{event.eventoId}</td>
                                                    <td>{event.titulo}</td>
                                                    <td>{(event.categoria === undefined) ? '' : event.categoria.titulo}</td>
                                                    <td>{(event.acessoLivre === false) ? 'não' : 'sim'}</td>
                                                    <td>{event.dataEvento}</td>
                                                    <td>{(event.localizacao === undefined) ? '' : event.localizacao.endereco}</td>
                                                    <td>
                                                        <button onClick={e => this.alterarEvento(event)}>Alterar</button>
                                                        <button onClick={e => this.deletarEvento(event.eventoId)}>Excluir</button>
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
                                Cadastrar Evento
                    </h2>
                            <form onSubmit={this.cadastrarEvento}>
                                <div className="container">

                                    <input
                                        type="text"
                                        className="class__evento"
                                        id="input__evento"
                                        placeholder="Evento"
                                        value={this.state.titulo}
                                        name="tituloEventoInput"
                                        onChange={this.atualizaEstado}


                                    />
                                 <select
                                        id="option__tipoevento"
                                        value={this.state.tituloCategoriaInput}
                                        name="tituloCategoriaInput"
                                        onChange={this.atualizaEstado}

                                        {
                                            ...this.state.listaCategorias.map(function (categoria) {
                                            return (
                                                <option key={categoria.categoriaId} value={categoria.categoriaId}>{categoria.titulo}</option>
                                            )
                                        })
                                        }
                                    /> 




                                    <select

                                        id="option_acesso"
                                        value={this.state.acessoLivre}
                                        name="acessoInput"
                                        onChange={this.atualizaEstado}
                                    >
                                        <option value="1">Livre</option>
                                        <option value="0">Restrito</option>
                                    </select>

                                    <input
                                        type="date"
                                        className="class__evento"
                                        id="input__evento"
                                        placeholder="Data"
                                        value={this.state.data}
                                        name="dataInput"
                                        onChange={this.atualizaEstado}


                                    />
                                    
                                            <select

                                            id="localizacaoEvento"
                                            value={this.state.localizacao}
                                            name="localizacaoInput"
                                            onChange={this.atualizaEstado}
                                        
                                        {

                                        ...this.state.localizacao.map (function(localizacao) {
                                            return (
                                                <option key={localizacao.localizacaoId} value={localizacao.localizacaoId}>{localizacao.endereco}</option>
                                            )
                                        })
                                    }
                                        
                                            
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

                <MDBContainer>
                    {/* <MDBBtn onClick={this.toggle}>Modal</MDBBtn> */}
                    <form onSubmit={this.salvarAlteracoes}>
                        <MDBModal isOpen={this.state.modal} toggle={this.toggle}>
                             <MDBModalHeader toggle={this.toggle}>Editar <b>{this.state.editarModal.titulo}</b> </MDBModalHeader> 
                            <MDBModalBody>
                                <MDBInput
                                    label="Evento"
                                 value={this.state.editarModal.titulo}
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

export default Eventos;