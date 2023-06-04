class ClientService {
    
    async getAll() {
        try {
            const response = await fetch(`clients`);
            if (response.ok) {
                const data = await response.json();
                return data;
            } else if (response.status === 400) {
                return response.json().then(errors => {
                    throw errors;
                });
            } else {
                throw new Error('Server Error');
            }
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    }

    async postData(data) {
        const response = await fetch(`clients`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else if (response.status === 400) {
                    return response.json().then(errors => {
                        throw errors;
                    });
                } else {
                    throw new Error('Server Error');
                }
            })
            .catch(error => {
                console.error('Error:', error.message);
                throw error;
            });
    }

    async updateData(clientId, updatedData) {
        try {
            const response = await fetch(`clients/${clientId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedData),
            });
            if (response.ok) {
            } else if (response.status === 400) {
                return response.json().then(errors => {
                    throw errors;
                });
            } else {
                throw new Error('Server Error');
            }
        } catch (error) {
            console.error('Error:', error.message);
            throw error;
        }
    }

    async deleteData(clientId) {
        try {
            const response = await fetch(`clients/${clientId}`, {
                method: 'DELETE',
            });
            if (response.ok) {
            } else if (response.status === 400) {
                return response.json().then(errors => {
                    throw errors;
                });
            } else {
                throw new Error('Server Error');
            }
        } catch (error) {
            console.error('Error:', error.message);
            throw error;
        }
    }
}

export default ClientService;
