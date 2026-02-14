import { Link } from "react-router-dom";

function App() {
    return (
        <>
            <div className="app">
                <Link to="/Pessoas">Pessoas</Link>
                <Link to="/Categorias">Categorias</Link>
            </div>
        </>
    );
}

export default App;
