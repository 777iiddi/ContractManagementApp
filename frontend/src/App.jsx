import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import { ToastContainer } from 'react-toastify';

// Importation des pages et composants
import LoginPage from './pages/LoginPage';
import ProtectedRoute from './components/ProtectedRoute';
import DashboardPage from './pages/DashboardPage';
import ManagerDashboardPage from './pages/ManagerDashboardPage';
import NewContractPage from './pages/NewContractPage';
import ContractDetailPage from './pages/ContractDetailPage';
import AdminPage from './pages/AdminPage';
import NewEmployeePage from './pages/NewEmployeePage'; // 1. Importer la nouvelle page
import ModelesPage from './pages/ModelesPage';
import ModeleDetailPage from './pages/ModeleDetailPage';

// Ce composant est la première page vue après la connexion.
// Son seul rôle est de rediriger vers le bon tableau de bord.
function HomePage() {
    const { userRole } = useAuth();

    // CORRECTION : On attend que le rôle soit chargé avant de rediriger.
    if (!userRole) {
        // Affiche un message de chargement pendant que le rôle est récupéré.
        return <div>Chargement de votre session...</div>;
    }

    if (userRole === 'Admin') {
        return <Navigate to="/admin" replace />;
    }
    if (userRole === 'Manager') {
        return <Navigate to="/dashboard-manager" replace />;
    }
    
    // Par défaut (pour RH), on redirige vers le tableau de bord principal.
    return <Navigate to="/dashboard-rh" replace />;
}


// Ce composant gère la structure de la page pour les utilisateurs connectés.
function Layout() {
    const { isAuthenticated, logout, userRole } = useAuth();
    return (
        <>
            {isAuthenticated && (
                <nav>
                    <Link to="/">Tableau de bord</Link>
                    {(userRole === 'RH' || userRole === 'Admin') && (
                        <> | <Link to="/contracts/new">Nouveau Contrat</Link>
                        |   <Link to="/modeles">Gérer les Modèles</Link></>
                    )}
                    {/* 2. AJOUT : Lien pour les Managers */}
                    {userRole === 'Manager' && (
                        <> | <Link to="/employees/new">Nouvel Employé</Link></>
                    )}
                    {userRole === 'Admin' && (
                        <> | <Link to="/admin">Administration</Link></>
                    )}
                    <button onClick={logout} style={{ marginLeft: '20px' }}>Déconnexion</button>
                </nav>
            )}
            <main>
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/dashboard-rh" element={<DashboardPage />} />
                    <Route path="/dashboard-manager" element={<ManagerDashboardPage />} />
                    <Route path="/contracts/new" element={<NewContractPage />} />
                    <Route path="/contracts/:contractId" element={<ContractDetailPage />} />
                    <Route path="/admin" element={<AdminPage />} />
                    <Route path="/employees/new" element={<NewEmployeePage />} />
                    <Route path="*" element={<Navigate to="/" />} />
                    <Route path="/modeles/new" element={<ModeleDetailPage />} />
                    <Route path="/modeles/:modeleId" element={<ModeleDetailPage />} />
                    <Route path="/modeles" element={<ModelesPage />} />
                </Routes>
            </main>
        </>
    );
}


// Le composant principal de l'application
function App() {
    return (
        <AuthProvider>
            <Router>
                <ToastContainer
                    position="top-right"
                    autoClose={5000}
                    hideProgressBar={false}
                />
                <Routes>
                    <Route path="/login" element={<LoginPage />} />
                    <Route element={<ProtectedRoute />}>
                        <Route path="/*" element={<Layout />} />
                    </Route>
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;
