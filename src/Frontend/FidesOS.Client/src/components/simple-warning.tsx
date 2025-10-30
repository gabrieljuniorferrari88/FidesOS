interface SimpleWarningProps {
  title: string;
  description: string;
}

export function SimpleWarning({ title, description }: SimpleWarningProps) {
  return (
    <div className="border border-red-300 rounded-lg p-4 mb-4 mt-1 border-opacity-25">
      <div className="flex items-start space-x-3">
        <div>
          <h4 className="text-red-500 font-semibold text-sm mb-1">{title}</h4>
          <p className="text-red-500 text-sm">{description}</p>
        </div>
      </div>
    </div>
  );
}
