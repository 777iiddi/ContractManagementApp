import React, { useState, useEffect, useCallback } from 'react';
import api from '../services/api'; // Utilise notre service API configuré !
import { toast } from 'react-toastify';

export default function AdminPage() {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [formData, setFormData] = useState({
        email: '',
        password: '',
        role: 'Manager',
    });

    const fetchUsers = useCallback(async () => {
        try {
            const response = await api.get('/Auth'); // Appelle le bon endpoint GET
            setUsers(response.data);
        } catch (error) {
            console.error("Erreur de récupération des utilisateurs:", error);
            toast.error("Impossible de charger la liste des utilisateurs.");
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchUsers();
    }, [fetchUsers]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.post('/Auth/register', formData);
            toast.success("Utilisateur créé avec succès !");
            setFormData({ email: '', password: '', role: 'Manager' }); // Réinitialiser le formulaire
            fetchUsers(); // Rafraîchir la liste
        } catch (error) {
            toast.error("Erreur lors de la création de l'utilisateur.");
        }
    };

    return (
        <div className="container">
            <div className="page-header">
                <h1>Panneau d'Administration</h1>
            </div>

            <div className="section card">
                <h2>Créer un Nouvel Utilisateur</h2>
                <form onSubmit={handleSubmit} className="user-form">
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input type="email" id="email" name="email" value={formData.email} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Mot de passe</label>
                        <input type="password" id="password" name="password" value={formData.password} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="role">Rôle</label>
                        <select id="role" name="role" value={formData.role} onChange={handleChange}>
                            <option value="Manager">Manager</option>
                            <option value="RH">RH</option>
                            <option value="Admin">Admin</option>
                        </select>
                    </div>
                    <button type="submit" className="button">Créer l'utilisateur</button>
                </form>
            </div>

            <div className="section">
                <h2>Liste des Utilisateurs</h2>
                <div className="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Email</th>
                                <th>Rôle</th>
                            </tr>
                        </thead>
                        <tbody>
                            {loading ? (
                                <tr><td colSpan="3">Chargement...</td></tr>
                            ) : (
                                users.map(user => (
                                    <tr key={user.id}>
                                        <td>{user.id}</td>
                                        <td>{user.email}</td>
                                        <td>{user.role}</td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}