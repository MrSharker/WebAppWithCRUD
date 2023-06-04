import React, { useState } from 'react';
import { validateEmail, validateCellphone } from './ValidationRules';

function ClientTable({ clients, handleUpdate, handleDelete }) {
    const [editingClientId, setEditingClientId] = useState(null);
    const [updatedData, setUpdatedData] = useState({});

    const handleEdit = (clientId) => {
        setEditingClientId(clientId);
        const client = clients.find((c) => c.id === clientId);
        setUpdatedData(client);
    };

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setUpdatedData((prevData) => ({ ...prevData, [name]: value }));
    };

    const handleSelectChange = (event) => {
        const { name, value } = event.target;
        setUpdatedData((prevData) => ({ ...prevData, [name]: parseInt(value) }));
    };

    const handleUpdateClick = (clientId) => {
        if (!validateEmail(updatedData.email)) {
            alert('Invalid email format');
            return;
        }

        if (!validateCellphone(updatedData.cellphone)) {
            alert('Invalid phone number format');
            return;
        }
        handleUpdate(clientId, updatedData);
        setEditingClientId(null);
        setUpdatedData({});
    };

    return (
        <table className="client-table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th>SMS Status</th>
                    <th>Email Status</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {clients.map((client) => (
                    <tr key={client.id}>
                        <td>{client.id}</td>
                        <td>
                            {editingClientId === client.id ? (
                                <input
                                    type="text"
                                    name="name"
                                    value={updatedData.name || ''}
                                    onChange={handleInputChange}
                                />
                            ) : (
                                client.name
                            )}
                        </td>
                        <td>
                            {editingClientId === client.id ? (
                                <input
                                    type="text"
                                    name="email"
                                    value={updatedData.email || ''}
                                    onChange={handleInputChange}
                                />
                            ) : (
                                client.email
                            )}
                        </td>
                        <td>
                            {editingClientId === client.id ? (
                                <input
                                    type="text"
                                    name="cellphone"
                                    value={updatedData.cellphone || ''}
                                    onChange={handleInputChange}
                                />
                            ) : (
                                    client.cellphone
                            )}
                        </td>
                        <td>
                            {editingClientId === client.id ? (
                                <select
                                    name="smsStatus"
                                    value={updatedData.smsStatus || ''}
                                    onChange={handleSelectChange}
                                >
                                    <option value={0}>Removed</option>
                                    <option value={1}>Active</option>
                                </select>
                            ) : (
                                client.smsStatus === 0 ? 'Removed' : 'Active'
                            )}
                        </td>
                        <td>
                            {editingClientId === client.id ? (
                                <select
                                    name="emailStatus"
                                    value={updatedData.emailStatus || ''}
                                    onChange={handleSelectChange}
                                >
                                    <option value={0}>Removed</option>
                                    <option value={1}>Active</option>
                                </select>
                            ) : (
                                client.emailStatus === 0 ? 'Removed' : 'Active'
                            )}
                        </td>
                        <td>
                            {editingClientId === client.id ? (
                                <>
                                    <button onClick={() => handleUpdateClick(client.id)}>Save</button>
                                    <button onClick={() => setEditingClientId(null)}>Cancel</button>
                                </>
                            ) : (
                                <>
                                    <button onClick={() => handleEdit(client.id)}>Edit</button>
                                    <button onClick={() => handleDelete(client.id)}>Delete</button>
                                </>
                            )}
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default ClientTable;
