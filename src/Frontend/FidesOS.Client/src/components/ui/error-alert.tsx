import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { Button } from "@/components/ui/button";
import { AlertCircle, X } from "lucide-react";
import { useState } from "react";

interface ErrorAlertProps {
  title?: string;
  errors: string[];
  onClose?: () => void;
}

export function ErrorAlert({
  title = "Erro",
  errors,
  onClose,
}: ErrorAlertProps) {
  const [isOpen, setIsOpen] = useState(true);

  const handleClose = () => {
    setIsOpen(false);
    onClose?.();
  };

  if (!isOpen) return null;

  return (
    <Alert variant="destructive" className="mb-4">
      <div className="flex items-start justify-between">
        <div className="flex items-start space-x-2 flex-1">
          <AlertCircle className="h-4 w-4 mt-0.5" />
          <div className="flex-1">
            <AlertTitle>{title}</AlertTitle>
            <AlertDescription>
              <ul className="list-disc list-inside space-y-1 mt-2">
                {errors.map((error, index) => (
                  <li key={index} className="text-sm">
                    {error}
                  </li>
                ))}
              </ul>
            </AlertDescription>
          </div>
        </div>
        <Button
          variant="ghost"
          size="sm"
          className="h-6 w-6 p-0 hover:bg-destructive/20"
          onClick={handleClose}
        >
          <X className="h-3 w-3" />
        </Button>
      </div>
    </Alert>
  );
}
