import React, { Component } from 'react';
import ClientForm, { Form } from './Form';
import ClientTable from './ClientTable';
import ClientService from '../API/ClientService';

const api = new ClientService();

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = {
            clients: [],
        };
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData = async () => {
        await api.getAll().then(responseData => {
            this.setState({ clients: responseData });
        })
            .catch(errors => {
                console.error('Error creating client:', errors);
            });
    }

    updateTable = async () => {
        try {
            const data = await api.getAll();
            this.setState({ clients: data });
        } catch (error) {
            // Handle error
        }
    }
    handleUpdate = async (clientId, updatedData) => {
        try {
            await api.updateData(clientId, updatedData);
            this.fetchData();
        } catch (error) {
            // Handle error
        }
    }

    handleDelete = async (clientId) => {
        try {
            await api.deleteData(clientId);
            this.fetchData();
        } catch (error) {
            // Handle error
        }
    }


render(){
    const { clients } = this.state;
    return (
        <div>
            <h1>Clients</h1>
            <ClientForm updateTable={this.updateTable} />
            <ClientTable
                clients={clients}
                handleUpdate={this.handleUpdate}
                handleDelete={this.handleDelete}
            />
        </div>
    );
  }
}
