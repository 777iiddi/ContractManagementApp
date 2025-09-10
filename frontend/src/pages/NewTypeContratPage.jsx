import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function NewTypeContratPage() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    
    const [formData, setFormData] = useState({
        nom: '',
        description: '',
        dureeDefautMois: '',
        periodeEssaiDefautJours: '',
        preavisDefautJours: '',
        champsObligatoires: []
    });

    const [nouveauChamp, setNouveauChamp] = useState({
        nomChamp: '',
        typeChamp: 'String',
        estRequis: true,
        messageErreur: ''
    });

    const typesChamp = [
        { value: 'String', label: 'Texte' },
        { value: 'Date', label: 'Date' },
        { value: 'Decimal', label: 'Nombre décimal' },
        { value: 'Integer', label: 'Nombre entier' },
        { value: 'Boolean', label: 'Oui/Non' }
    ];

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value === '' ? null : value
        }));
    };

    const handleChampChange = (e) => {
        const { name, value, type, checked } = e.target;
        setNouveauChamp(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const ajouterChamp = () => {
        if (!nouveauChamp.nomChamp.trim()) {
            toast.error('Le nom du champ est requis');
            return;
        }

        setFormData(prev => ({
            ...prev,
            champsObligatoires: [...prev.champsObligatoires, { ...nouveauChamp }]
        }));

        setNouveauChamp({
            nomChamp: '',
            typeChamp: 'String',
            estRequis: true,
            messageErreur: ''
        });
    };

    const supprimerChamp = (index) => {
        setFormData(prev => ({
            ...prev,
            champsObligatoires: prev.champsObligatoires.filter((_, i) => i !== index)
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (loading) return;

        if (!formData.nom.trim()) {
            toast.error('Le nom du type de contrat est requis');
            return;
        }

        setLoading(true);
        try {
            const response = await api.post('/TypeContrats', formData);
            toast.success('Type de contrat créé avec succès');
            navigate('/type-contrats');
        } catch (error) {
            console.error('Erreur lors de la création', error);
            toast.error('Erreur lors de la création');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
            <div style={{ marginBottom: '20px' }}>
                <button 
                    onClick={() => navigate('/type-contrats')}
                    style={{
                        backgroundColor: '#6c757d',
                        color: 'white',
                        padding: '8px 16px',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                        marginBottom: '10px'
                    }}
                >
                    ← Retour à la liste
                </button>
                <h1>Nouveau Type de Contrat</h1>
            </div>

            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: '20px', padding: '15px', border: '1px solid #ddd', borderRadius: '4px' }}>
                    <h3>Informations générales</h3>
                    
                    <div style={{ marginBottom: '15px' }}>
                        <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                            Nom du type de contrat *
                        </label>
                        <input
                            type="text"
                            name="nom"
                            value={formData.nom}
                            onChange={handleInputChange}
                            required
                            placeholder="ex: CDI, CDD, Stage..."
                            style={{ width: '100%', padding: '8px', border: '1px solid #ccc', borderRadius: '4px' }}
                        />
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                            Description
                        </label>
                        <textarea
                            name="description"
                            value={formData.description}
                            onChange={handleInputChange}
                            placeholder="Description du type de contrat..."
                            rows="3"
                            style={{ width: '100%', padding: '8px', border: '1px solid #ccc', borderRadius: '4px' }}
                        />
                    </div>

                    <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: '15px' }}>
                        <div>
                            <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                                Durée par défaut (mois)
                            </label>
                            <input
                                type="number"
                                name="dureeDefautMois"
                                value={formData.dureeDefautMois}
                                onChange={handleInputChange}
                                placeholder="Laissez vide pour indéterminée"
                                style={{ width: '100%', padding: '8px', border: '1px solid #ccc', borderRadius: '4px' }}
                            />
                        </div>

                        <div>
                            <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                                Période d'essai (jours)
                            </label>
                            <input
                                type="number"
                                name="periodeEssaiDefautJours"
                                value={formData.periodeEssaiDefautJours}
                                onChange={handleInputChange}
                                style={{ width: '100%', padding: '8px', border: '1px solid #ccc', borderRadius: '4px' }}
                            />
                        </div>

                        <div>
                            <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                                Préavis (jours)
                            </label>
                            <input
                                type="number"
                                name="preavisDefautJours"
                                value={formData.preavisDefautJours}
                                onChange={handleInputChange}
                                style={{ width: '100%', padding: '8px', border: '1px solid #ccc', borderRadius: '4px' }}
                            />
                        </div>
                    </div>
                </div>

                {/* Champs obligatoires */}
                <div style={{ marginBottom: '20px', padding: '15px', border: '1px solid #ddd', borderRadius: '4px' }}>
                    <h3>Champs obligatoires</h3>
                    
                    <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '10px', marginBottom: '15px' }}>
                        <div>
                            <label style={{ display: 'block', marginBottom: '5px' }}>Nom du champ</label>
                            <input
                                type="text"
                                name="nomChamp"
                                value={nouveauChamp.nomChamp}
                                onChange={handleChampChange}
                                placeholder="ex: SalaireBase, DateFin..."
                                style={{ width: '100%', padding: '6px', border: '1px solid #ccc', borderRadius: '4px' }}
                            />
                        </div>

                        <div>
                            <label style={{ display: 'block', marginBottom: '5px' }}>Type</label>
                            <select
                                name="typeChamp"
                                value={nouveauChamp.typeChamp}
                                onChange={handleChampChange}
                                style={{ width: '100%', padding: '6px', border: '1px solid #ccc', borderRadius: '4px' }}
                            >
                                {typesChamp.map(type => (
                                    <option key={type.value} value={type.value}>
                                        {type.label}
                                    </option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label style={{ display: 'block', marginBottom: '5px' }}>Message d'erreur</label>
                        <input
                            type="text"
                            name="messageErreur"
                            value={nouveauChamp.messageErreur}
                            onChange={handleChampChange}
                            placeholder="Message si validation échoue"
                            style={{ width: '100%', padding: '6px', border: '1px solid #ccc', borderRadius: '4px' }}
                        />
                    </div>

                    <div style={{ marginBottom: '15px' }}>
                        <label>
                            <input
                                type="checkbox"
                                name="estRequis"
                                checked={nouveauChamp.estRequis}
                                onChange={handleChampChange}
                            />
                            Champ obligatoire
                        </label>
                    </div>

                    <button
                        type="button"
                        onClick={ajouterChamp}
                        style={{
                            backgroundColor: '#28a745',
                            color: 'white',
                            padding: '8px 16px',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer'
                        }}
                    >
                        Ajouter le champ
                    </button>

                    {/* Liste des champs ajoutés */}
                    {formData.champsObligatoires.length > 0 && (
                        <div style={{ marginTop: '20px' }}>
                            <h4>Champs configurés :</h4>
                            <table style={{ width: '100%', borderCollapse: 'collapse', border: '1px solid #ddd' }}>
                                <thead>
                                    <tr style={{ backgroundColor: '#f8f9fa' }}>
                                        <th style={{ padding: '8px', border: '1px solid #ddd' }}>Champ</th>
                                        <th style={{ padding: '8px', border: '1px solid #ddd' }}>Type</th>
                                        <th style={{ padding: '8px', border: '1px solid #ddd' }}>Requis</th>
                                        <th style={{ padding: '8px', border: '1px solid #ddd' }}>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {formData.champsObligatoires.map((champ, index) => (
                                        <tr key={index}>
                                            <td style={{ padding: '8px', border: '1px solid #ddd' }}>{champ.nomChamp}</td>
                                            <td style={{ padding: '8px', border: '1px solid #ddd' }}>{champ.typeChamp}</td>
                                            <td style={{ padding: '8px', border: '1px solid #ddd' }}>
                                                {champ.estRequis ? 'Oui' : 'Non'}
                                            </td>
                                            <td style={{ padding: '8px', border: '1px solid #ddd' }}>
                                                <button
                                                    type="button"
                                                    onClick={() => supprimerChamp(index)}
                                                    style={{
                                                        backgroundColor: '#dc3545',
                                                        color: 'white',
                                                        padding: '4px 8px',
                                                        border: 'none',
                                                        borderRadius: '4px',
                                                        cursor: 'pointer'
                                                    }}
                                                >
                                                    Supprimer
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>

                {/* Boutons d'action */}
                <div style={{ textAlign: 'right' }}>
                    <button
                        type="button"
                        onClick={() => navigate('/type-contrats')}
                        style={{
                            backgroundColor: '#6c757d',
                            color: 'white',
                            padding: '10px 20px',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer',
                            marginRight: '10px'
                        }}
                    >
                        Annuler
                    </button>
                    <button
                        type="submit"
                        disabled={loading}
                        style={{
                            backgroundColor: '#007bff',
                            color: 'white',
                            padding: '10px 20px',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: loading ? 'not-allowed' : 'pointer'
                        }}
                    >
                        {loading ? 'Création...' : 'Créer le type de contrat'}
                    </button>
                </div>
            </form>
        </div>
    );
}
