import React, { useState } from 'react';
import ClientService from '../API/ClientService';
import { validateEmail, validateCellphone } from './ValidationRules';

const api = new ClientService();

function ClientForm({ updateTable }) {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [cellphone, setCellphone] = useState('');
    const [errors, setErrors] = useState([]);

    async function handleSubmit(event) {
        event.preventDefault();

        if (!validateEmail(email)) {
            setErrors([{ fieldName: 'email', errorDetail: 'Invalid email format' }]);
            return;
        }

        if (!validateCellphone(cellphone)) {
            setErrors([{ fieldName: 'cellphone', errorDetail: 'Invalid phone number format' }]);
            return;
        }

        const newClient = {
            name: name,
            email: email,
            cellphone: cellphone,
        };

        await api.postData(newClient).then(responseData => {
            console.log('Client created:', responseData);
            setName('');
            setEmail('');
            setCellphone('');
            setErrors([]);

            updateTable();
        })
            .catch(errors => {
                console.error('Error creating client:', errors);
                setErrors(errors);
            });
    }

    return (
        <form onSubmit={handleSubmit} className="client-form">
            {errors.length > 0 && (
                <div className="error-container">
                    {errors.map((error, index) => (
                        <p key={index} className="error-message">
                            {error.errorDetail}
                        </p>
                    ))}
                </div>
            )}
            <label>
                Name:
                <input type="text" value={name} onChange={(event) => setName(event.target.value)} required />
            </label>
            <label>
                Email:
                <input type="email" value={email} onChange={(event) => setEmail(event.target.value)} required />
            </label>
            <label>
                Cellphone:
                <input type="tel" value={cellphone} onChange={(event) => setCellphone(event.target.value)} required />
            </label>
            <button type="submit" className="submit-button">Create Client</button>
        </form>
    );
}

export default ClientForm;
