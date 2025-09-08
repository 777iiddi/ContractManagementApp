import React, { useState, useEffect, useCallback } from 'react';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function AdminPage() {
    const [users, setUsers] = useState([]);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('Manager');

    // On utilise useCallback pour s'assurer que la fonction n'est pas recréée à chaque rendu
    const fetchUsers = useCallback(async () => {
        try {
            const response = await api.get('/Auth');
            setUsers(response.data);
        } catch (error) {
            console.error("Erreur lors de la récupération des utilisateurs", error);
            toast.error("Impossible de charger la liste des utilisateurs.");
        }
    }, []);

    useEffect(() => {
        fetchUsers();
    }, [fetchUsers]);

    const handleCreateUser = async (e) => {
        e.preventDefault();
        try {
            await api.post('/Auth/register', { email, password, role });
            toast.success('Utilisateur créé avec succès !');
            setEmail('');
            setPassword('');
            // On appelle à nouveau fetchUsers pour rafraîchir la liste
            await fetchUsers();
        } catch (error) {
            console.error("Erreur lors de la création de l'utilisateur", error);
            toast.error(error.response?.data?.message || "Erreur lors de la création.");
        }
    };

    return (
        <div>
            <div className="page-header">
                <h1>Panneau d'Administration</h1>
            </div>

            <div className="form-container">
                <h2>Créer un Nouvel Utilisateur</h2>
                <form onSubmit={handleCreateUser} className="user-form">
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" required />
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Mot de passe" required />
                    <select value={role} onChange={(e) => setRole(e.target.value)}>
                        <option value="Manager">Manager</option>
                        <option value="RH">RH</option>
                        <option value="Admin">Admin</option>
                    </select>
                    <button type="submit">Créer l'utilisateur</button>
                </form>
            </div>

            <div className="table-container" style={{ marginTop: '2rem' }}>
                <h2>Liste des Utilisateurs</h2>
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Email</th>
                            <th>Rôle</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(user => (
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.email}</td>
                                <td>{user.role}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
