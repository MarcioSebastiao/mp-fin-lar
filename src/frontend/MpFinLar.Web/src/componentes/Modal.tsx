import React from "react";
import "./Modal.css";

interface ModalProps {
    aberto: boolean;
    aoFechar: () => void;
    children: React.ReactNode;
}

function Modal({ aberto, aoFechar, children }: ModalProps) {
    if (!aberto) return null;

    return (
        <div className="modal" onClick={aoFechar}>
            <div className="modal-conteudo" onClick={(e) => e.stopPropagation()}>
                <button className="modal-fechar" onClick={aoFechar}>
                    x
                </button>
                {children}
            </div>
        </div>
    );
}
export default Modal;
